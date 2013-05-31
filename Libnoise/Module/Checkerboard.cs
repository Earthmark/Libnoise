using System;

namespace Noise.Module
{
	public class Checkerboard : Module
	{
		public override double GetValue(double x, double y, double z)
		{
			var ix = (int)(Math.Floor(NoiseGen.MakeInt32Range(x)));
			var iy = (int)(Math.Floor(NoiseGen.MakeInt32Range(y)));
			var iz = (int)(Math.Floor(NoiseGen.MakeInt32Range(z)));
			return (ix & 1 ^ iy & 1 ^ iz & 1) != 0 ? -1.0 : 1.0;
		}
	}
}
