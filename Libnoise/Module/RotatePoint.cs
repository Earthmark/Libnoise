using System;

namespace Noise.Module
{
	public class RotatePoint : Module
	{
		public const double DefaultRotateX = 0.0;
		public const double DefaultRotateY = 0.0;
		public const double DefaultRotateZ = 0.0;

		private double x1Matrix;
		private double x2Matrix;
		private double x3Matrix;
		private double y1Matrix;
		private double y2Matrix;
		private double y3Matrix;
		private double z1Matrix;
		private double z2Matrix;
		private double z3Matrix;
		private double xAngle;
		private double yAngle;
		private double zAngle;

		private double XAngle
		{
			get { return xAngle; }
			set
			{
				xAngle = value;
				SetAngles(xAngle, yAngle, zAngle);
			}
		}

		private double YAngle
		{
			get { return yAngle; }
			set
			{
				yAngle = value;
				SetAngles(xAngle, yAngle, zAngle);
			}
		}

		private double ZAngle
		{
			get { return zAngle; }
			set
			{
				zAngle = value;
				SetAngles(xAngle, yAngle, zAngle);
			}
		}

		public Module ConnectedModule { get; set; }

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

		private void SetAngles(double xAng, double yAng, double zAng)
		{
			var xCos = Math.Cos(xAng * MathConsts.DegToRad);
			var yCos = Math.Cos(yAng * MathConsts.DegToRad);
			var zCos = Math.Cos(zAng * MathConsts.DegToRad);
			var xSin = Math.Sin(xAng * MathConsts.DegToRad);
			var ySin = Math.Sin(yAng * MathConsts.DegToRad);
			var zSin = Math.Sin(zAng * MathConsts.DegToRad);

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

		public override double GetValue(double x, double y, double z)
		{
			var nx = (x1Matrix * x) + (y1Matrix * y) + (z1Matrix * z);
			var ny = (x2Matrix * x) + (y2Matrix * y) + (z2Matrix * z);
			var nz = (x3Matrix * x) + (y3Matrix * y) + (z3Matrix * z);
			return ConnectedModule.GetValue(nx, ny, nz);
		}
	}
}
