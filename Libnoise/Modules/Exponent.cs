using System;

namespace Noise.Modules
{
	public class Exponent : Module
	{
		public const double DefaultPower = 1.0;

		public Exponent()
		{
			Expon = DefaultPower;
		}

		public Exponent(double expon)
		{
			Expon = expon;
		}

		public Exponent(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public Exponent(Module connectedModule, double expon)
		{
			ConnectedModule = connectedModule;
			Expon = expon;
		}

		public Module ConnectedModule { get; set; }
		public double Expon { get; set; }

		public override double GetValue(double x, double y, double z)
		{
			double value = ConnectedModule.GetValue(x, y, z);
			return (Math.Pow(Math.Abs((value + 1.0) / 2.0), Expon) * 2.0 - 1.0);
		}
	}
}