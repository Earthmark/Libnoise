using System;

namespace Noiselib.Modules
{
	public class Cylinders : Module
	{
		public const double DefaultCylindersFrequency = 1.0;

		public Cylinders()
		{
			Frequency = DefaultCylindersFrequency;
		}

		public Cylinders(double frequency)
		{
			Frequency = frequency;
		}

		public double Frequency { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				x *= Frequency;
				z *= Frequency;

				double distFromCenter = Math.Sqrt(x * x + z * z);
				double distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
				double distFromLargerSphere = 1.0 - distFromSmallerSphere;
				double nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
				return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range.
			}
		}
	}
}