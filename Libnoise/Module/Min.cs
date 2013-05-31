using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Module
{
	public class Min : Module
	{
		public Module SourceModule1 { get; set; }
		public Module SourceModule2 { get; set; }

		public Min() {}

		public Min(Module sourceModule1, Module sourceModule2)
		{
			SourceModule2 = sourceModule2;
			SourceModule1 = sourceModule1;
		}

		public override double GetValue(double x, double y, double z)
		{
			var v0 = SourceModule1.GetValue(x, y, z);
			var v1 = SourceModule2.GetValue(x, y, z);
			return Math.Min(v0, v1);
		}
	}
}
