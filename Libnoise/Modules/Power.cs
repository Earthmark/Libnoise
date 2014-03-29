using System;

namespace Noise.Modules
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

		public override double GetValue(double x, double y, double z)
		{
			return Math.Pow(SourceModuleBase.GetValue(x, y, z), SourceModuleExponent.GetValue(x, y, z));
		}
	}
}