using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Module
{
	public class Power : Module
	{
		public Module SourceModuleBase { get; set; }
		public Module SourceModuleExponent { get; set; }

		public Power() {}

		public Power(Module sourceModuleBase, Module sourceModuleExponent)
		{
			SourceModuleExponent = sourceModuleExponent;
			SourceModuleBase = sourceModuleBase;
		}

		public override double GetValue(double x, double y, double z)
		{
			return Math.Pow(SourceModuleBase.GetValue(x, y, z), SourceModuleExponent.GetValue(x, y, z));
		}
	}
}
