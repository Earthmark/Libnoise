using System;

namespace Noiselib.Models
{
	public static class Model
	{
		/// <summary>
		/// Returns the output value from the noise module given the
		/// (latitude, longitude) coordinates of the specified input value
		/// located on the surface of the sphere.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Use a negative latitude if the input value is located on the
		/// southern hemisphere.
		/// </para>
		/// <para>
		/// Use a negative longitude if the input value is located on the
		/// western hemisphere.
		/// </para>
		/// </remarks>
		/// <param name="lat">The latitude of the input value, in degrees.</param>
		/// <param name="lon">The longitude of the input value, in degrees.</param>
		/// <param name="x">The output x value.</param>
		/// <param name="y">The output y value.</param>
		/// <param name="z">The output z value.</param>
		public static void Sphere(double lat, double lon, out double x, out double y, out double z)
		{
			Misc.LatLonToXYZ(lat, lon, out x, out y, out z);
		}

		/// <summary>
		/// Returns the output value from the noise module given the
		/// (angle, height) coordinates of the specified input value located
		/// on the surface of the cylinder.
		/// </summary>
		/// <remarks>
		/// This cylinder has a radius of 1.0 unit and has infinite height.
		/// It is oriented along the y axis.  Its center is located at the
		/// origin.
		/// </remarks>
		/// <param name="angle">The angle around the cylinder's center, in degrees.</param>
		/// <param name="height">The height along the y axis.</param>
		/// <returns>The output value from the noise module.</returns>
		public static void Cylinder(double angle, double height, out double x, out double y, out double z)
		{
			x = Math.Cos(angle * MathConsts.DegToRad);
			y = height;
			z = Math.Sin(angle * MathConsts.DegToRad);
		}
	}
}
