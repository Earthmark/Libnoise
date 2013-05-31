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

	public static class NoiseGen
	{

		// These constants control certain parameters that all coherent-noise
		// functions require.
#if NOISE_VERSION1
		// Constants used by the original version of libnoise.
		// Because X_NOISE_GEN is not relatively prime to the other values, and
		// Z_NOISE_GEN is close to 256 (the number of random gradient vectors),
		// patterns show up in high-frequency coherent noise.
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

		public static int IntValueNoise3D(int x, int y, int z, int seed = 0)
		{
			// All constants are primes and must remain prime in order for this noise
			// function to work correctly.
			var n = (XNoiseGen * x + YNoiseGen * y + ZNoiseGen * z + SeedNoiseGen * seed) & 0x7fffffff;
			n = (n >> 13) ^ n;
			return (n * (n * n * 60493 + 19990303) + 1376312589) & 0x7fffffff;
		}

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

		public static double ValueNoise3D(int x, int y, int z, int seed = 0)
		{
			return 1.0 - (IntValueNoise3D(x, y, z, seed) / 1073741824.0);
		}
	}
}

