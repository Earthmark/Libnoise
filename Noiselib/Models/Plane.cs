using Noiselib.Modules;

namespace Noiselib.Models
{
	/// <summary>
	/// Model that defines the surface of a plane.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This model returns an output value from a noise module given the
	/// coordinates of an input value located on the surface of an
	/// ( x, z ) plane.
	/// </para>
	/// <para>
	/// To generate an output value, pass the ( x, z ) coordinates of
	/// an input value to the GetValue method.
	/// </para>
	/// <para>
	/// This model is useful for creating two-dimensional textures or terrain height maps for local areas.
	/// </para>
	/// <para>
	/// This plane extends infinitely in both directions.
	/// </para>
	/// </remarks>
	public class Plane
	{
		/// <summary>
		/// Constructor, does not bind a method.
		/// </summary>
		public Plane() {}

		/// <summary>
		/// Constructor, binds a method to the plane.
		/// </summary>
		/// <param name="sourceMethod">The method to be encapsulated.</param>
		public Plane(Module sourceMethod)
		{
			SourceMethod = sourceMethod;
		}

		/// <summary>
		/// The method encapsulated by the plane.
		/// </summary>
		public Module SourceMethod { get; set; }

		/// <summary>
		/// Returns the output value from the noise method given the
		/// ( x, z ) coordinates of the specified input value located
		/// on the surface of the plane.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value from the noise method.</returns>
		public double GetValue(double x, double z)
		{
			return SourceMethod(x, 0, z);
		}
	}
}
