namespace Noise.Module
{
	public class Clamp : Module
	{
		public const double DefaultClampLowerBound = -1.0;
		public const double DefaultClampUpperBound = 1.0;

		public Module ConnectedModule { get; set; }

		public double LowerBound { get; set; }
		public double UpperBound { get; set; }

		public Clamp()
		{
			LowerBound = DefaultClampLowerBound;
			UpperBound = DefaultClampUpperBound;
		}

		public Clamp(Module connectedModule)
		{
			ConnectedModule = connectedModule;
			LowerBound = DefaultClampLowerBound;
			UpperBound = DefaultClampUpperBound;
		}

		public Clamp(double lowerBound, double upperBound)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;
		}

		public Clamp(Module connectedModule, double lowerBound, double upperBound)
		{
			ConnectedModule = connectedModule;
			LowerBound = lowerBound;
			UpperBound = upperBound;
		}

		public override double GetValue(double x, double y, double z)
		{
			var value = ConnectedModule.GetValue(x, y, z);
			return value < LowerBound ? LowerBound : (value > UpperBound ? UpperBound : value);
		}
	}
}
