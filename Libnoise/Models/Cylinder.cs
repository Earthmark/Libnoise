using System;
using Noise.Modules;

namespace Noise.Models
{
	public class Cylinder
	{
		public Cylinder() {}

		public Cylinder(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public Module ConnectedModule { get; set; }

		private double GetValue(double angle, double height)
		{
			var x = Math.Cos(angle * MathConsts.DegToRad);
			var y = height;
			var z = Math.Sin(angle * MathConsts.DegToRad);
			return ConnectedModule.GetValue(x, y, z);
		}
	}
}
