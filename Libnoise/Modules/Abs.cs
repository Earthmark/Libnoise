using System;

namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs the absolute value of the output value from a noise module.
	/// </summary>
	public class Abs : Module
	{
		/// <summary>
		/// Creates this noise module without setting the ConnectedModule.
		/// </summary>
		public Abs() {}

		/// <summary>
		/// Creates this noise module, also setting the ConnectedModule.
		/// </summary>
		/// <param name="sourceModule">The noise module to connect to this module.</param>
		public Abs(Module sourceModule)
		{
			SourceModule = sourceModule;
		}

		/// <summary>
		/// The module this noise module is connected to.
		/// </summary>
		public Module SourceModule { get; set; }

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double GetValue(double x, double y, double z)
		{
			return Math.Abs(SourceModule.GetValue(x, y, z));
		}
	}
}
