namespace Noiselib.Modules
{
	public class TranslatePoint : Module
	{
		public const double DefaultTranslatePointX = 0.0;
		public const double DefaultTranslatePointY = 0.0;
		public const double DefaultTranslatePointZ = 0.0;

		public TranslatePoint()
		{
			XTranslation = DefaultTranslatePointX;
			YTranslation = DefaultTranslatePointY;
			ZTranslation = DefaultTranslatePointZ;
		}

		public TranslatePoint(Module connectedModule)
		{
			ConnectedModule = connectedModule;
			XTranslation = DefaultTranslatePointX;
			YTranslation = DefaultTranslatePointY;
			ZTranslation = DefaultTranslatePointZ;
		}
		public TranslatePoint(double xTranslation, double yTranslation, double zTranslation)
		{
			XTranslation = xTranslation;
			YTranslation = yTranslation;
			ZTranslation = zTranslation;
		}

		public TranslatePoint(Module connectedModule, double xTranslation, double yTranslation, double zTranslation)
		{
			ConnectedModule = connectedModule;
			XTranslation = xTranslation;
			YTranslation = yTranslation;
			ZTranslation = zTranslation;
		}

		public double XTranslation { get; set; }
		public double YTranslation { get; set; }
		public double ZTranslation { get; set; }

		public Module ConnectedModule { get; set; }

		public void SetTranslation(double xTranslation, double yTranslation, double zTranslation)
		{
			XTranslation = xTranslation;
			YTranslation = yTranslation;
			ZTranslation = zTranslation;
		}

		private void SetTranslation(double translation)
		{
			XTranslation = translation;
			YTranslation = translation;
			ZTranslation = translation;
		}

		public override double GetValue(double x, double y, double z)
		{
			return ConnectedModule.GetValue(x + XTranslation, y + YTranslation, z + ZTranslation);
		}
	}
}
