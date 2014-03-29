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

		public override double this[double x, double y, double z]
		{
			get { return Math.Pow(SourceModuleBase[x, y, z], SourceModuleExponent[x, y, z]); }
		}
	}
}
