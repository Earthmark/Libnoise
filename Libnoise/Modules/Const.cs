namespace Noise.Modules
{
	public class Const : Module
	{
		const double DefaultConstValue = 0.0;

		public Const()
		{
			ConstValue = DefaultConstValue;
		}

		public Const(double constValue)
		{
			ConstValue = constValue;
		}

		public double ConstValue { get; set; }

		public override double this[double x, double y, double z]
		{
			get { return ConstValue; }
		}
	}
}
