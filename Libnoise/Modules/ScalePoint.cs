namespace Noise.Modules
{
	public class ScalePoint : Module
	{
		public const double DefaultScalePointX = 1.0;

		public const double DefaultScalePointY = 1.0;

		public const double DefaultScalePointZ = 1.0;

		public ScalePoint()
		{
			XScale = DefaultScalePointX;
			YScale = DefaultScalePointY;
			ZScale = DefaultScalePointZ;
		}

		public ScalePoint(Module connectedModule)
		{
			XScale = DefaultScalePointX;
			YScale = DefaultScalePointY;
			ZScale = DefaultScalePointZ;
			ConnectedModule = connectedModule;
		}

		public ScalePoint(double xScale, double yScale, double zScale)
		{
			XScale = xScale;
			YScale = yScale;
			ZScale = zScale;
		}

		public ScalePoint(Module connectedModule, double xScale, double yScale, double zScale)
		{
			XScale = xScale;
			YScale = yScale;
			ZScale = zScale;
			ConnectedModule = connectedModule;
		}

		public double XScale { get; set; }
		public double YScale { get; set; }
		public double ZScale { get; set; }

		public Module ConnectedModule { get; set; }

		public void SetScale(double scale)
		{
			XScale = scale;
			YScale = scale;
			ZScale = scale;
		}

		public void SetScale(double xScale, double yScale, double zScale)
		{
			XScale = xScale;
			YScale = yScale;
			ZScale = zScale;
		}

		public override double GetValue(double x, double y, double z)
		{
			return ConnectedModule.GetValue(x * XScale, y * YScale, z * ZScale);
		}
	}
}
