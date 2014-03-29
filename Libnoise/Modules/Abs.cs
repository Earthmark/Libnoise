using System;

namespace Noise.Modules
{
	/// <summary>
	///      A <see cref="Module" /> that outputs the absolute value of the output value from a <see cref="Module" />.
	/// </summary>
	public class Abs : Module
	{
		/// <summary>
		///      Creates a <see cref="Module" /> without setting the NoiseModule.
		/// </summary>
		public Abs() {}

		/// <summary>
		///      Creates a <see cref="Module" />, while also setting the NoiseModule.
		/// </summary>
		/// <param name="noiseModule">The <see cref="Module" /> to connect to this <see cref="Abs" /> noise <see cref="Module" />.</param>
		public Abs(Module noiseModule)
		{
			NoiseModule = noiseModule;
		}

		/// <summary>
		///      The noise <see cref="Module" /> this <see cref="Abs" /> noise <see cref="Module" /> is connected to.
		/// </summary>
		public Module NoiseModule { get; set; }

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <exception cref="NullReferenceException">NoiseModule was not set to an instance.</exception>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double GetValue(double x, double y, double z)
		{
			if(NoiseModule == null)
			{
				throw new NullReferenceException("NoiseModule was not set to an object.");
			}
			return Math.Abs(NoiseModule.GetValue(x, y, z));
		}
	}
}