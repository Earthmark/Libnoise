using System;

namespace Noiselib.Modules
{
	public class Min : Module
	{
		public Min() {}

		public Min(Module sourceModule1, Module sourceModule2)
		{
			SourceModule2 = sourceModule2;
			SourceModule1 = sourceModule1;
		}

		public Module SourceModule1 { get; set; }
		public Module SourceModule2 { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				double v0 = SourceModule1[x, y, z];
				double v1 = SourceModule2[x, y, z];
				return Math.Min(v0, v1);
			}
		}

		public override double this[double x, double y]
		{
			get
			{
				double v0 = SourceModule1[x, y];
				double v1 = SourceModule2[x, y];
				return Math.Min(v0, v1);
			}
		}
	}
}