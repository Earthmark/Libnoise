namespace Noise.Modules
{
	/// <summary>
	/// Abstract base class for noise modules.
	/// </summary>
	public abstract class Module
	{
		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public abstract double GetValue(double x, double y, double z);
	}
}
