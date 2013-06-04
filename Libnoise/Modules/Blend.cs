namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs a weighted blend of the output values from two source modules given the output value supplied by a control module.
	/// </summary>
	public class Blend : Module
	{
		/// <summary>
		/// Creates a new <see cref="Blend"/> <see cref="Module"/> without setting the NoiseModule1 or NoiseModule2.
		/// </summary>
		public Blend() {}

		/// <summary>
		/// Creates a new <see cref="Blend"/> <see cref="Module"/>, also setting the NoiseModule1 or NoiseModule2.
		/// </summary>
		/// <param name="noiseModule1">The first <see cref="Module"/> to connect to this <see cref="Blend"/> <see cref="Module"/>.</param>
		/// <param name="noiseModule2">The second <see cref="Module"/> to connect to this <see cref="Blend"/> <see cref="Module"/>.</param>
		/// <param name="controlModule">The control <see cref="Module"/> determines the weight of the blending operation. See <see cref="ControlModule"/> for further usage.</param>
		public Blend(Module noiseModule1, Module noiseModule2, Module controlModule)
		{
			NoiseModule1 = noiseModule1;
			NoiseModule2 = noiseModule2;
			ControlModule = controlModule;
		}

		/// <summary>
		/// Outputs one of the values to blend.
		/// </summary>
		public Module NoiseModule1 { get; set; }

		/// <summary>
		/// Outputs one of the values to blend.
		/// </summary>
		public Module NoiseModule2 { get; set; }

		/// <summary>
		/// The control module determines the weight of the blending operation.
		/// </summary>
		/// <remarks>
		/// Negative values weigh the blend towards the output value from <see cref="NoiseModule1"/>.
		/// Positive values weigh the blend towards the output value from <see cref="NoiseModule2"/>.
		/// </remarks>
		public Module ControlModule { get; set; }

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double GetValue(double x, double y, double z)
		{
			var v0 = NoiseModule1.GetValue(x, y, z);
			var v1 = NoiseModule2.GetValue(x, y, z);
			var alpha = (ControlModule.GetValue(x, y, z) + 1.0) / 2.0;
			return Interp.LinearInterp(v0, v1, alpha);
		}
	}
}
