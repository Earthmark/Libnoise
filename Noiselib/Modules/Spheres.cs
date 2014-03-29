using System;

namespace Noiselib.Modules
{
	public class Spheres : Module
	{
		public const double DefaultSpheresFrequency = 1.0;

		public Spheres()
		{
			Frequency = DefaultSpheresFrequency;
		}

		public Spheres(double frequency)
		{
			Frequency = frequency;
		}

		public double Frequency { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				x *= Frequency;
				y *= Frequency;
				z *= Frequency;

				double distFromCenter = Math.Sqrt(x * x + y * y + z * z);
				double distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
				double distFromLargerSphere = 1.0 - distFromSmallerSphere;
				double nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
				return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range.
			}
		}
	}
}