using System;

namespace Noise
{
	public static class Latlon
	{
		public static void LatLonToXYZ(double lat, double lon, out double x, out double y, out double z)
		{
			var r = Math.Cos(MathConsts.DegToRad * lat);
			x = r * Math.Cos(MathConsts.DegToRad * lon);
			y = Math.Sin(MathConsts.DegToRad * lat);
			z = r * Math.Sin(MathConsts.DegToRad * lon);
		}
	}
}
