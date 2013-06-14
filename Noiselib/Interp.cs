namespace Noiselib
{
	/// <summary>
	/// A class containing different interpolation styles.
	/// </summary>
	public static class Interp
	{
		/// <summary>
		/// Performs cubic interpolation between two values bound between two other values.
		/// </summary>
		/// <remarks>
		/// The alpha value should range from 0.0 to 1.0.  If the alpha value is
		/// 0.0, this function returns n1. If the alpha value is 1.0, this
		/// function returns n2.
		/// </remarks>
		/// <param name="n0">The value before the first value.</param>
		/// <param name="n1">The first value.</param>
		/// <param name="n2">The second value.</param>
		/// <param name="n3">The value after the second value.</param>
		/// <param name="a">The alpha value.</param>
		/// <returns>The interpolated value.</returns>
		public static double CubicInterp(double n0, double n1, double n2, double n3, double a)
		{
			var p = (n3 - n2) - (n0 - n1);
			var q = (n0 - n1) - p;
			var r = n2 - n0;
			var s = n1;
			return p * a * a * a + q * a * a + r * a + s;
		}
		
		/// <summary>
		/// Performs linear interpolation between two values.
		/// </summary>
		/// <remarks>
		/// The alpha value should range from 0.0 to 1.0.  If the alpha value is
		/// 0.0, this function returns n0.  If the alpha value is 1.0, this
		/// function returns n1.
		/// </remarks>
		/// <param name="n0">The first value.</param>
		/// <param name="n1">The second value.</param>
		/// <param name="a">The alpha value.</param>
		/// <returns>The interpolated value.</returns>
		public static double LinearInterp(double n0, double n1, double a)
		{
			return ((1.0 - a) * n0) + (a * n1);
		}

		/// <summary>
		/// Maps a value onto a cubic S-curve.
		/// </summary>
		/// <remarks>
		/// <para>
		/// a should range from 0.0 to 1.0.
		/// </para>
		/// <para>
		/// The derivitive of a cubic S-curve is zero at a = 0.0 and a = 1.0
		/// </para>
		/// </remarks>
		/// <param name="a">The value to map onto a cubic S-curve.</param>
		/// <returns>The mapped value.</returns>
		public static double SCurve3(double a)
		{
			return (a * a * (3.0 - 2.0 * a));
		}

		/// <summary>
		/// Maps a value onto a quintic S-curve.
		/// </summary>
		/// <remarks>
		/// <para>
		/// a should range from 0.0 to 1.0.
		/// </para>
		/// <para>
		/// The first derivitive of a quintic S-curve is zero at a = 0.0 and a = 1.0
		/// </para>
		/// <para>
		/// The second derivitive of a quintic S-curve is zero at a = 0.0 and a = 1.0
		/// </para>
		/// </remarks>
		/// <param name="a">The value to map onto a quintic S-curve.</param>
		/// <returns>The mapped value.</returns>
		public static double SCurve5(double a)
		{
			var a3 = a * a * a;
			var a4 = a3 * a;
			var a5 = a4 * a;
			return (6.0 * a5) - (15.0 * a4) + (10.0 * a3);
		}
	}
}
