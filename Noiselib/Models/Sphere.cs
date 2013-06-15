using Noiselib.Modules;

namespace Noiselib.Models
{
	/// <summary>
	/// Model that defines the surface of a sphere.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This model returns an output value from a noise method given the
	/// coordinates of an input value located on the surface of a sphere.
	/// </para>
	/// <para>
	/// To generate an output value, pass the (latitude, longitude)
	/// coordinates of an input value to the GetValue() method.
	/// </para>
	/// <para>
	/// This model is useful for creating
	/// seamless textures that can be mapped onto a sphere,
	/// terrain height maps for entire planets
	/// </para>
	/// <para>
	/// This sphere has a radius of 1.0 unit and its center is located at
	/// the origin.
	/// </para>
	/// </remarks>
	public class Sphere
	{
		/// <summary>
		/// Constructor, does not bind a method.
		/// </summary>
		public Sphere() {}

		/// <summary>
		/// Constructor, binds a source method.
		/// </summary>
		/// <param name="sourceMethod">The method to encapsulate.</param>
		public Sphere(Module sourceMethod)
		{
			SourceMethod = sourceMethod;
		}

		/// <summary>
		/// The encapsulated method.
		/// </summary>
		public Module SourceMethod { get; set; }

		/// <summary>
		/// Returns the output value from the noise method given the
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
		/// <returns>The output value from the noise method.</returns>
		public double GetValue(double lat, double lon)
		{
			double x, y, z;
			Misc.LatLonToXYZ(lat, lon, out x, out y, out z);
			return SourceMethod(x, y, z);
		}
	}
}
