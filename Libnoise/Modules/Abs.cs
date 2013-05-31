using System;

namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs the absolute value of the output value from a noise module.
	/// </summary>
	public class Abs : Module
	{
		/// <summary>
		/// The module this noise module is connected to.
		/// </summary>
		public Module ConnectedModule { get; set; }

		/// <summary>
		/// Creates this noise module without setting the ConnectedModule.
		/// </summary>
		public Abs() {}

		/// <summary>
		/// Creates this noise module, also setting the ConnectedModule.
		/// </summary>
		/// <param name="connectedModule">The noise module to connect to this module.</param>
		public Abs(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double GetValue(double x, double y, double z)
		{
			return Math.Abs(ConnectedModule.GetValue(x, y, z));
		}
	}
}
