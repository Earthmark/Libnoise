using System;

namespace Noiselib.Modules
{
	public class Power : Module
	{
		public Power() {}

		public Power(Module sourceModuleBase, Module sourceModuleExponent)
		{
			SourceModuleExponent = sourceModuleExponent;
			SourceModuleBase = sourceModuleBase;
		}

		public Module SourceModuleBase { get; set; }
		public Module SourceModuleExponent { get; set; }

		public override double this[double x, double y, double z]
		{
			get { return Math.Pow(SourceModuleBase[x, y, z], SourceModuleExponent[x, y, z]); }
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x, double y]
		{
			get { return Math.Pow(SourceModuleBase[x, y], SourceModuleExponent[x, y]); }
		}
	}
}