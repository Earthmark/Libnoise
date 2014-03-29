using System;

namespace Noiselib.Modules
{
	public class RotatePoint : Module
	{
		public const double DefaultRotateX = 0.0;
		public const double DefaultRotateY = 0.0;
		public const double DefaultRotateZ = 0.0;

		private double x1Matrix;
		private double x2Matrix;
		private double x3Matrix;
		private double xAngle;
		private double y1Matrix;
		private double y2Matrix;
		private double y3Matrix;
		private double yAngle;
		private double z1Matrix;
		private double z2Matrix;
		private double z3Matrix;
		private double zAngle;

		public RotatePoint()
		{
			SetAngles(DefaultRotateX, DefaultRotateY, DefaultRotateZ);
		}

		public RotatePoint(Module connectedModule)
		{
			ConnectedModule = connectedModule;
			SetAngles(DefaultRotateX, DefaultRotateY, DefaultRotateZ);
		}

		public RotatePoint(double xAngle, double yAngle, double zAngle)
		{
			SetAngles(xAngle, yAngle, zAngle);
		}

		public RotatePoint(Module connectedModule, double xAngle, double yAngle, double zAngle)
		{
			ConnectedModule = connectedModule;
			SetAngles(xAngle, yAngle, zAngle);
		}

		public double XAngle
		{
			get { return xAngle; }
			set
			{
				xAngle = value;
				SetAngles(xAngle, yAngle, zAngle);
			}
		}

		public double YAngle
		{
			get { return yAngle; }
			set
			{
				yAngle = value;
				SetAngles(xAngle, yAngle, zAngle);
			}
		}

		public double ZAngle
		{
			get { return zAngle; }
			set
			{
				zAngle = value;
				SetAngles(xAngle, yAngle, zAngle);
			}
		}

		public Module ConnectedModule { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				double nx = (x1Matrix * x) + (y1Matrix * y) + (z1Matrix * z);
				double ny = (x2Matrix * x) + (y2Matrix * y) + (z2Matrix * z);
				double nz = (x3Matrix * x) + (y3Matrix * y) + (z3Matrix * z);
				return ConnectedModule[nx, ny, nz];
			}
		}

		private void SetAngles(double xAng, double yAng, double zAng)
		{
			double xCos = Math.Cos(xAng * MathConsts.DegToRad);
			double yCos = Math.Cos(yAng * MathConsts.DegToRad);
			double zCos = Math.Cos(zAng * MathConsts.DegToRad);
			double xSin = Math.Sin(xAng * MathConsts.DegToRad);
			double ySin = Math.Sin(yAng * MathConsts.DegToRad);
			double zSin = Math.Sin(zAng * MathConsts.DegToRad);

			x1Matrix = ySin * xSin * zSin + yCos * zCos;
			y1Matrix = xCos * zSin;
			z1Matrix = ySin * zCos - yCos * xSin * zSin;
			x2Matrix = ySin * xSin * zCos - yCos * zSin;
			y2Matrix = xCos * zCos;
			z2Matrix = -yCos * xSin * zCos - ySin * zSin;
			x3Matrix = -ySin * xCos;
			y3Matrix = xSin;
			z3Matrix = yCos * xCos;

			XAngle = xAng;
			YAngle = yAng;
			ZAngle = zAng;
		}
	}
}