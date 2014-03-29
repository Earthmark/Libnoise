using System;

namespace Noise.Modules
{
	public class Max : Module
	{
		public Max() {}

		public Max(Module sourceModule1, Module sourceModule2)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
		}

		public Module SourceModule1 { get; set; }
		public Module SourceModule2 { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				var v0 = SourceModule1[x, y, z];
				var v1 = SourceModule2[x, y, z];
				return Math.Max(v0, v1);
			}
		}
	}
}
