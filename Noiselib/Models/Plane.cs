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
		/// Constructor, does not bind a module.
		/// </summary>
		public Plane() {}

		/// <summary>
		/// Constructor, binds a module to the plane.
		/// </summary>
		/// <param name="sourceModule">The module to be encapsulated.</param>
		public Plane(Module sourceModule)
		{
			SourceModule = sourceModule;
		}

		/// <summary>
		/// The module encapsulated by the plane.
		/// </summary>
		public Module SourceModule { get; set; }

		/// <summary>
		/// Returns the output value from the noise module given the
		/// ( x, z ) coordinates of the specified input value located
		/// on the surface of the plane.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value from the noise module.</returns>
		public double GetValue(double x, double z)
		{
			return SourceModule.GetValue(x, 0, z);
		}
	}
}
