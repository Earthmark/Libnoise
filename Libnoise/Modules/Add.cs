namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs the sum of the two output values from two source modules.
	/// </summary>
	public class Add : Module
	{
		/// <summary>
		/// Creates this noise module without setting the ConnectedModule1 or ConnectedModule2.
		/// </summary>
		public Add() {}

		/// <summary>
		/// Creates this noise module, also setting the ConnectedModule.
		/// </summary>
		/// <param name="sourceModule1">The first noise module to connect to this module.</param>
		/// <param name="sourceModule2">The second noise module to connect to this module.</param>
		public Add(Module sourceModule1, Module sourceModule2)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
		}

		/// <summary>
		/// The first module this noise module is connected to.
		/// </summary>
		public Module SourceModule1 { get; set; }

		/// <summary>
		/// The second module this noise module is connected to.
		/// </summary>
		public Module SourceModule2 { get; set; }

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double GetValue(double x, double y, double z)
		{
			return SourceModule1.GetValue(x, y, z) + SourceModule2.GetValue(x, y, z);
		}
	}
}
