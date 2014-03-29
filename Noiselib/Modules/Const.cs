namespace Noiselib.Modules
{
	public class Const : Module
	{
		private const double DefaultConstValue = 0.0;

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

		public override double this[double x, double y]
		{
			get { return ConstValue; }
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x]
		{
			get { return ConstValue; }
		}
	}
}