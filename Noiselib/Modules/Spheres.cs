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

		public override double this[double x, double y]
		{
			get
			{
				x *= Frequency;
				y *= Frequency;

				double distFromCenter = Math.Sqrt(x * x + y * y);
				double distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
				double distFromLargerSphere = 1.0 - distFromSmallerSphere;
				double nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
				return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range.}
			}
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x]
		{
			get
			{
				x *= Frequency;

				double distFromCenter = x;
				double distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
				double distFromLargerSphere = 1.0 - distFromSmallerSphere;
				double nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
				return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range.}}
			}
		}
	}
}