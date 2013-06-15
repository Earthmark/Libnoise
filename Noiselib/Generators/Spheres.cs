using System;
using Noiselib.Modules;

namespace Noiselib.Generators
{
	public sealed class Spheres
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

		public double this[double x, double y, double z]
		{
			get { return GetValue(x, y, z); }
		}

		public double GetValue(double x, double y, double z)
		{
			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			var distFromCenter = Math.Sqrt(x * x + y * y + z * z);
			var distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
			var distFromLargerSphere = 1.0 - distFromSmallerSphere;
			var nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
			return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range.
		}
	}
}
