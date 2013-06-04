namespace Noise
{
	/// <summary>
	/// Enumerates the noise quality.
	/// </summary>
	public enum NoiseQuality
	{
		/// <summary>
		/// Generates coherent noise quickly.  When a coherent-noise function with
		/// this quality setting is used to generate a bump-map image, there are
		/// noticeable "creasing" artifacts in the resulting image.  This is
		/// because the derivative of that function is discontinuous at integer
		/// boundaries.
		/// </summary>
		Fast = 0,

		/// <summary>
		/// Generates standard-quality coherent noise.  When a coherent-noise
		/// function with this quality setting is used to generate a bump-map
		/// image, there are some minor "creasing" artifacts in the resulting
		/// image.  This is because the second derivative of that function is
		/// discontinuous at integer boundaries.
		/// </summary>
		Standard = 1,

		/// <summary>
		/// Generates the best-quality coherent noise.  When a coherent-noise
		/// function with this quality setting is used to generate a bump-map
		/// image, there are no "creasing" artifacts in the resulting image.  This
		/// is because the first and second derivatives of that function are
		/// continuous at integer boundaries.
		/// </summary>
		Best = 2
	}

	/// <summary>
	/// The container for noise generation methods.
	/// </summary>
	public static class NoiseGen
	{
		// These constants control certain parameters that all coherent-noise
		// functions require.

		// Constants used by the original version of libnoise.
		// Because XNoiseGen is not relatively prime to the other values, and
		// ZNoiseGen is close to 256 (the number of random gradient vectors),
		// patterns show up in high-frequency coherent noise.
#if NOISE_VERSION1
		const int XNoiseGen = 1;
		const int YNoiseGen = 31337;
		const int ZNoiseGen = 263;
		const int SeedNoiseGen = 1013;
		const int ShiftNoiseGen = 13;
#else
		// Constants used by the current version of libnoise.
		private const int XNoiseGen = 1619;
		private const int YNoiseGen = 31337;
		private const int ZNoiseGen = 6971;
		private const int SeedNoiseGen = 1013;
		private const int ShiftNoiseGen = 8;
#endif

		/// <summary>
		/// Generates a gradient-coherent-noise value from the coordinates of a
		/// three-dimensional input value.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The return value ranges from -1.0 to +1.0.
		/// </para>
		/// <para>
		/// For an explanation of the difference between gradient noise and
		/// value noise, see the comments for the <see cref="GradientNoise3D"/> function.
		/// </para>
		/// </remarks>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <param name="seed">The random number seed.</param>
		/// <param name="noiseQuality">The quality of the coherent-noise.</param>
		/// <returns>The generated gradient-coherent-noise value.</returns>
		public static double GradientCoherentNoise3D(double x, double y, double z, int seed = 0, NoiseQuality noiseQuality = NoiseQuality.Standard)
		{
			// Create a unit-length cube aligned along an integer boundary.  This cube
			// surrounds the input point.
			var x0 = (x > 0.0 ? (int) x : (int) x - 1);
			var x1 = x0 + 1;
			var y0 = (y > 0.0 ? (int) y : (int) y - 1);
			var y1 = y0 + 1;
			var z0 = (z > 0.0 ? (int) z : (int) z - 1);
			var z1 = z0 + 1;

			// Map the difference between the coordinates of the input value and the
			// coordinates of the cube's outer-lower-left vertex onto an S-curve.
			double xs = 0, ys = 0, zs = 0;
			switch(noiseQuality)
			{
				case NoiseQuality.Fast:
					xs = (x - x0);
					ys = (y - y0);
					zs = (z - z0);
					break;
				case NoiseQuality.Standard:
					xs = Interp.SCurve3(x - x0);
					ys = Interp.SCurve3(y - y0);
					zs = Interp.SCurve3(z - z0);
					break;
				case NoiseQuality.Best:
					xs = Interp.SCurve5(x - x0);
					ys = Interp.SCurve5(y - y0);
					zs = Interp.SCurve5(z - z0);
					break;
			}

			// Now calculate the noise values at each vertex of the cube.  To generate
			// the coherent-noise value at the input point, interpolate these eight
			// noise values using the S-curve value as the interpolant (trilinear
			// interpolation.)
			var n0 = GradientNoise3D(x, y, z, x0, y0, z0, seed);
			var n1 = GradientNoise3D(x, y, z, x1, y0, z0, seed);
			var ix0 = Interp.LinearInterp(n0, n1, xs);
			n0 = GradientNoise3D(x, y, z, x0, y1, z0, seed);
			n1 = GradientNoise3D(x, y, z, x1, y1, z0, seed);
			var ix1 = Interp.LinearInterp(n0, n1, xs);
			var iy0 = Interp.LinearInterp(ix0, ix1, ys);
			n0 = GradientNoise3D(x, y, z, x0, y0, z1, seed);
			n1 = GradientNoise3D(x, y, z, x1, y0, z1, seed);
			ix0 = Interp.LinearInterp(n0, n1, xs);
			n0 = GradientNoise3D(x, y, z, x0, y1, z1, seed);
			n1 = GradientNoise3D(x, y, z, x1, y1, z1, seed);
			ix1 = Interp.LinearInterp(n0, n1, xs);
			var iy1 = Interp.LinearInterp(ix0, ix1, ys);

			return Interp.LinearInterp(iy0, iy1, zs);
		}

		/// <summary>
		/// Generates a gradient-noise value from the coordinates of a
		/// three-dimensional input value and the integer coordinates of a
		/// nearby three-dimensional value.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The difference between <paramref name="fx"/> and <paramref name="ix"/> must be less than or equal to one.
		/// </para>
		/// <para>
		/// The difference between <paramref name="fy"/> and <paramref name="iy"/> must be less than or equal to one.
		/// </para>
		/// <para>
		/// The difference between <paramref name="fz"/> and <paramref name="iz"/> must be less than or equal to one.
		/// </para>
		///
		/// <para>
		/// A gradient-noise function generates better-quality noise than a
		/// value-noise function.  Most noise modules use gradient noise for
		/// this reason, although it takes much longer to calculate.
		/// </para>
		/// <para>
		/// The return value ranges from -1.0 to +1.0.
		///	</para>
		/// 
		/// <para>
		/// This function generates a gradient-noise value by performing the
		/// following steps:
		///	</para>
		/// <para>
		/// - It first calculates a random normalized vector based on the
		///   nearby integer value passed to this function.
		/// </para>
		/// <para>
		/// - It then calculates a new value by adding this vector to the
		///   nearby integer value passed to this function.
		/// </para>
		/// <para>
		/// - It then calculates the dot product of the above-generated value
		///   and the floating-point input value passed to this function.
		/// </para>
		/// <para>
		/// A noise function differs from a random-number generator because it
		/// always returns the same output value if the same input value is passed
		/// to it.
		/// </para>
		/// </remarks>
		/// <param name="fx">The floating-point x coordinate of the input value.</param>
		/// <param name="fy">The floating-point y coordinate of the input value.</param>
		/// <param name="fz">The floating-point z coordinate of the input value.</param>
		/// <param name="ix">The integer x coordinate of a nearby value.</param>
		/// <param name="iy">The integer y coordinate of a nearby value.</param>
		/// <param name="iz">The integer z coordinate of a nearby value.</param>
		/// <param name="seed">The random number seed.</param>
		/// <returns>The generated gradient-noise value.</returns>
		public static double GradientNoise3D(double fx, double fy, double fz, int ix, int iy, int iz, int seed = 0)
		{
			// Randomly generate a gradient vector given the integer coordinates of the
			// input value.  This implementation generates a random number and uses it
			// as an index into a normalized-vector lookup table.
			var vectorIndex = (XNoiseGen * ix + YNoiseGen * iy + ZNoiseGen * iz + SeedNoiseGen * seed) & 0xffffffff;
			vectorIndex ^= (vectorIndex >> ShiftNoiseGen);
			vectorIndex &= 0xff;

			var xvGradient = Misc.RandomVectors[(vectorIndex << 2)];
			var yvGradient = Misc.RandomVectors[(vectorIndex << 2) + 1];
			var zvGradient = Misc.RandomVectors[(vectorIndex << 2) + 2];

			// Set up us another vector equal to the distance between the two vectors
			// passed to this function.
			var xvPoint = (fx - ix);
			var yvPoint = (fy - iy);
			var zvPoint = (fz - iz);

			// Now compute the dot product of the gradient vector with the distance
			// vector.  The resulting value is gradient noise.  Apply a scaling value
			// so that this noise value ranges from -1.0 to 1.0.
			return ((xvGradient * xvPoint) + (yvGradient * yvPoint) + (zvGradient * zvPoint)) * 2.12;
		}

		/// <summary>
		/// Generates an integer-noise value from the coordinates of a
		/// three-dimensional input value.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The return value ranges from 0 to 2147483647.
		/// </para>
		/// <para>
		/// A noise function differs from a random-number generator because it
		/// always returns the same output value if the same input value is passed
		/// to it.
		/// </para>
		/// </remarks>
		/// <param name="x">The integer x coordinate of the input value.</param>
		/// <param name="y">The integer y coordinate of the input value.</param>
		/// <param name="z">The integer z coordinate of the input value.</param>
		/// <param name="seed">A random number seed.</param>
		/// <returns>The generated integer-noise value.</returns>
		public static int IntValueNoise3D(int x, int y, int z, int seed = 0)
		{
			// All constants are primes and must remain prime in order for this noise
			// function to work correctly.
			var n = (XNoiseGen * x + YNoiseGen * y + ZNoiseGen * z + SeedNoiseGen * seed) & 0x7fffffff;
			n = (n >> 13) ^ n;
			return (n * (n * n * 60493 + 19990303) + 1376312589) & 0x7fffffff;
		}

		/// <summary>
		/// Modifies a floating-point value so that it can be stored in a
		/// int32 variable.
		/// </summary>
		/// <remarks>
		/// This function does not modify n.
		///
		/// In libnoise, the noise-generating algorithms are all integer-based;
		/// they use variables of type int32.  Before calling a noise
		/// function, pass the x, y, and z coordinates to this function to
		/// ensure that these coordinates can be cast to a int32 value.
		///
		/// Although you could do a straight cast from double to int32, the
		/// resulting value may differ between platforms.  By using this function,
		/// you ensure that the resulting value is identical between platforms.
		/// </remarks>
		/// <param name="n">A floating-point number.</param>
		/// <returns>The modified floating-point number.</returns>
		public static double MakeInt32Range(double n)
		{
			if(n >= 1073741824.0)
			{
				return (2.0 * (n % 1073741824.0)) - 1073741824.0;
			}
			if(n <= -1073741824.0)
			{
				return (2.0 * (n % 1073741824.0)) + 1073741824.0;
			}
			return n;
		}

		/// <summary>
		/// Generates a value-coherent-noise value from the coordinates of a
		/// three-dimensional input value.
		/// </summary>
		/// <remarks>
		/// The return value ranges from -1.0 to +1.0.
		///
		/// For an explanation of the difference between gradient noise and
		/// value noise, see the comments for the <see cref="GradientNoise3D"/> function.
		/// </remarks>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <param name="seed">The random number seed.</param>
		/// <param name="noiseQuality">The quality of the coherent-noise.</param>
		/// <returns>The generated value-coherent-noise value.</returns>
		public static double ValueCoherentNoise3D(double x, double y, double z, int seed = 0, NoiseQuality noiseQuality = NoiseQuality.Standard)
		{
			// Create a unit-length cube aligned along an integer boundary.  This cube
			// surrounds the input point.
			var x0 = (x > 0.0 ? (int) x : (int) x - 1);
			var x1 = x0 + 1;
			var y0 = (y > 0.0 ? (int) y : (int) y - 1);
			var y1 = y0 + 1;
			var z0 = (z > 0.0 ? (int) z : (int) z - 1);
			var z1 = z0 + 1;

			// Map the difference between the coordinates of the input value and the
			// coordinates of the cube's outer-lower-left vertex onto an S-curve.
			double xs = 0, ys = 0, zs = 0;
			switch(noiseQuality)
			{
				case NoiseQuality.Fast:
					xs = (x - x0);
					ys = (y - y0);
					zs = (z - z0);
					break;
				case NoiseQuality.Standard:
					xs = Interp.SCurve3(x - x0);
					ys = Interp.SCurve3(y - y0);
					zs = Interp.SCurve3(z - z0);
					break;
				case NoiseQuality.Best:
					xs = Interp.SCurve5(x - x0);
					ys = Interp.SCurve5(y - y0);
					zs = Interp.SCurve5(z - z0);
					break;
			}

			// Now calculate the noise values at each vertex of the cube.  To generate
			// the coherent-noise value at the input point, interpolate these eight
			// noise values using the S-curve value as the interpolant (trilinear
			// interpolation.)
			var n0 = ValueNoise3D(x0, y0, z0, seed);
			var n1 = ValueNoise3D(x1, y0, z0, seed);
			var ix0 = Interp.LinearInterp(n0, n1, xs);
			n0 = ValueNoise3D(x0, y1, z0, seed);
			n1 = ValueNoise3D(x1, y1, z0, seed);
			var ix1 = Interp.LinearInterp(n0, n1, xs);
			var iy0 = Interp.LinearInterp(ix0, ix1, ys);
			n0 = ValueNoise3D(x0, y0, z1, seed);
			n1 = ValueNoise3D(x1, y0, z1, seed);
			ix0 = Interp.LinearInterp(n0, n1, xs);
			n0 = ValueNoise3D(x0, y1, z1, seed);
			n1 = ValueNoise3D(x1, y1, z1, seed);
			ix1 = Interp.LinearInterp(n0, n1, xs);
			var iy1 = Interp.LinearInterp(ix0, ix1, ys);
			return Interp.LinearInterp(iy0, iy1, zs);
		}

		/// <summary>
		/// Generates a value-noise value from the coordinates of a
		/// three-dimensional input value.
		/// </summary>
		/// <remarks>
		/// The return value ranges from -1.0 to +1.0.
		///
		/// A noise function differs from a random-number generator because it
		/// always returns the same output value if the same input value is passed
		/// to it.
		/// </remarks>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <param name="seed">A random number seed.</param>
		/// <returns>The generated value-noise value.</returns>
		public static double ValueNoise3D(int x, int y, int z, int seed = 0)
		{
			return 1.0 - (IntValueNoise3D(x, y, z, seed) / 1073741824.0);
		}
	}
}
