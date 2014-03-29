namespace Noiselib.Modules
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

		public ScaleBias(Module connectedModule, double scale, double bias)
		{
			ConnectedModule = connectedModule;
			Scale = scale;
			Bias = bias;
		}

		public double Bias { get; set; }
		public double Scale { get; set; }
		public Module ConnectedModule { get; set; }

		public override double this[double x, double y, double z]
		{
			get { return ConnectedModule[x, y, z] * Scale + Bias; }
		}
	}
}