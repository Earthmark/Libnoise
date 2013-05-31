namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs the sum of the two output values from two source modules.
	/// </summary>
	public class Add : Module
	{
		/// <summary>
		/// The first module this noise module is connected to.
		/// </summary>
		public Module ConnectedModule1 { get; set; }

		/// <summary>
		/// The second module this noise module is connected to.
		/// </summary>
		public Module ConnectedModule2 { get; set; }
		
		/// <summary>
		/// Creates this noise module without setting the ConnectedModule1 or ConnectedModule2.
		/// </summary>
		public Add() {}

		/// <summary>
		/// Creates this noise module, also setting the ConnectedModule.
		/// </summary>
		/// <param name="connectedModule1">The first noise module to connect to this module.</param>
		/// <param name="connectedModule2">The second noise module to connect to this module.</param>
		public Add(Module connectedModule1, Module connectedModule2)
		{
			ConnectedModule1 = connectedModule1;
			ConnectedModule2 = connectedModule2;
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
			return ConnectedModule1.GetValue(x, y, z) + ConnectedModule2.GetValue(x, y, z);
		}
	}
}
