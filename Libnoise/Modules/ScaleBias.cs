namespace Noise.Modules
{
	public class ScaleBias : Module
	{
		public const double DefaultBias = 0.0;
		public const double DefaultScale = 1.0;

		public ScaleBias()
		{
			Bias = DefaultBias;
			Scale = DefaultScale;
		}

		public ScaleBias(Module connectedModule)
		{
			ConnectedModule = connectedModule;
			Bias = DefaultBias;
			Scale = DefaultScale;
		}

		public ScaleBias(Module connectedModule, double bias, double scale)
		{
			ConnectedModule = connectedModule;
			Bias = bias;
			Scale = scale;
		}

		public double Bias { get; set; }
		public double Scale { get; set; }
		public Module ConnectedModule { get; set; }

		public override double GetValue(double x, double y, double z)
		{
			return ConnectedModule.GetValue(x, y, z) * Scale + Bias;
		}
	}
}
