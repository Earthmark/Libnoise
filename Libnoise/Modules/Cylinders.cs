using System;

namespace Noise.Modules
{
	public class Cylinders : Module
	{
		public const double DefaultCylindersFrequency = 1.0;

		public double Frequency { get; set; }

		public Cylinders()
		{
			Frequency = DefaultCylindersFrequency;
		}

		public Cylinders(double frequency)
		{
			Frequency = frequency;
		}

		public override double GetValue(double x, double y, double z)
		{
			x *= Frequency;
			z *= Frequency;

			var distFromCenter = Math.Sqrt(x * x + z * z);
			var distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
			var distFromLargerSphere = 1.0 - distFromSmallerSphere;
			var nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
			return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range.
		}
	}
}
