namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs a weighted blend of the output values from two source modules given the output value supplied by a control module.
	/// </summary>
	public class Blend : Module
	{
		/// <summary>
		/// Creates a new Blend instance with the noise modules as null.
		/// </summary>
		public Blend() {}

		/// <summary>
		/// Creates a new Blend instance with the specified noise modules.
		/// </summary>
		/// <param name="sourceModule1">Outputs one of the values to blend.</param>
		/// <param name="sourceModule2">Outputs one of the values to blend.</param>
		/// <param name="controlModule">The control module determines the weight of the blending operation. See <see cref="ControlModule"/> for further usage.</param>
		public Blend(Module sourceModule1, Module sourceModule2, Module controlModule)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
			ControlModule = controlModule;
		}

		/// <summary>
		/// Outputs one of the values to blend.
		/// </summary>
		public Module SourceModule1 { get; set; }

		/// <summary>
		/// Outputs one of the values to blend.
		/// </summary>
		public Module SourceModule2 { get; set; }

		/// <summary>
		/// The control module determines the weight of the blending operation.
		/// </summary>
		/// <remarks>
		/// Negative values weigh the blend towards the output value from <see cref="SourceModule1"/>. Positive values weigh the blend towards the output value from <see cref="SourceModule2"/>.
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
			var v0 = SourceModule1.GetValue(x, y, z);
			var v1 = SourceModule2.GetValue(x, y, z);
			var alpha = (ControlModule.GetValue(x, y, z) + 1.0) / 2.0;
			return Interp.LinearInterp(v0, v1, alpha);
		}
	}
}
