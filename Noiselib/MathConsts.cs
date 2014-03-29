using System;

namespace Noiselib
{
	/// <summary>
	///      A set of globally usefull math constants.
	/// </summary>
	public static class MathConsts
	{
		/// <summary>
		///      Square root of 2.
		/// </summary>
		public const double Sqrt2 = 1.4142135623730950488;

		/// <summary>
		///      Square root of 3.
		/// </summary>
		public const double Sqrt3 = 1.7320508075688772935;

		/// <summary>
		///      Converts an angle from degrees to radians.
		/// </summary>
		public const double DegToRad = Math.PI / 180.0;

		/// <summary>
		///      Converts an angle from radians to degrees.
		/// </summary>
		public const double RadToDeg = 1.0 / DegToRad;
	}
}