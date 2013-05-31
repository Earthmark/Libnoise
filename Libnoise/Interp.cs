namespace Noise
{
	public static class Interp
	{
		public static double CubicInterp(double n0, double n1, double n2, double n3, double a)
		{
			var p = (n3 - n2) - (n0 - n1);
			var q = (n0 - n1) - p;
			var r = n2 - n0;
			var s = n1;
			return p * a * a * a + q * a * a + r * a + s;
		}
		
		public static double LinearInterp(double n0, double n1, double a)
		{
			return ((1.0 - a) * n0) + (a * n1);
		}

		public static double SCurve3(double a)
		{
			return (a * a * (3.0 - 2.0 * a));
		}

		public static double SCurve5(double a)
		{
			var a3 = a * a * a;
			var a4 = a3 * a;
			var a5 = a4 * a;
			return (6.0 * a5) - (15.0 * a4) + (10.0 * a3);
		}
	}
}
