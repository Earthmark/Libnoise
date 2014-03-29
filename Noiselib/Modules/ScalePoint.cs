﻿namespace Noiselib.Modules
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

		public override double this[double x, double y, double z]
		{
			get { return ConnectedModule[x * XScale, y * YScale, z * ZScale]; }
		}

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
	}
}