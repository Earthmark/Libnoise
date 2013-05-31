namespace Noise.Module
{
	public class Const : Module
	{
		const double DefaultConstValue = 0.0;

		public double ConstValue { get; set; }

		public Const()
		{
			ConstValue = DefaultConstValue;
		}

		public Const(double constValue)
		{
			ConstValue = constValue;
		}

		public override double GetValue(double x, double y, double z)
		{
			return ConstValue;
		}
	}
}
