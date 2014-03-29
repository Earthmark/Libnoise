using System;

namespace Noiselib.Modules
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

		public override double this[double x, double y, double z]
		{
			get
			{
				double value = ConnectedModule[x, y, z];
				return (Math.Pow(Math.Abs((value + 1.0) / 2.0), Expon) * 2.0 - 1.0);
			}
		}

		public override double this[double x, double y]
		{
			get
			{
				double value = ConnectedModule[x, y];
				return (Math.Pow(Math.Abs((value + 1.0) / 2.0), Expon) * 2.0 - 1.0);
			}
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x]
		{
			get
			{
				double value = ConnectedModule[x];
				return (Math.Pow(Math.Abs((value + 1.0) / 2.0), Expon) * 2.0 - 1.0);
			}
		}
	}
}