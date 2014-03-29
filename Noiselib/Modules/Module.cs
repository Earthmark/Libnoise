namespace Noiselib.Modules
{
	/// <summary>
	///      Abstract base class for noise modules.
	/// </summary>
	public abstract class Module
	{
		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public abstract double this[double x, double y, double z] { get; }

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public abstract double this[double x, double y] { get; }

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public abstract double this[double x] { get; }
	}
}