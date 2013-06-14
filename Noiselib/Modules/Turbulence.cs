﻿using Noiselib.Generators;

namespace Noiselib.Modules
{
	public sealed class Turbulence
	{
		public const double DefaultTurbulenceFrequency = Perlin.DefaultPerlinFrequency;
		public const double DefaultTurbulencePower = 1.0;
		public const int DefaultTurbulenceRoughness = 3;
		public const int DefaultTurbulenceSeed = Perlin.DefaultPerlinSeed;

		private readonly Perlin xDistortModule;
		private readonly Perlin yDistortModule;
		private readonly Perlin zDistortModule;

		public Turbulence()
		{
			zDistortModule = new Perlin();
			yDistortModule = new Perlin();
			xDistortModule = new Perlin();
			Power = DefaultTurbulencePower;
			Seed = DefaultTurbulenceSeed;
			Frequency = DefaultTurbulenceFrequency;
			Roughness = DefaultTurbulenceRoughness;
		}

		public Turbulence(double power, double frequency, int roughness, int seed)
		{
			zDistortModule = new Perlin();
			yDistortModule = new Perlin();
			xDistortModule = new Perlin();
			Power = power;
			Seed = seed;
			Frequency = frequency;
			Roughness = roughness;
		}

		/// The power (scale) of the displacement.
		public double Power { get; set; }

		public int Roughness
		{
			get { return xDistortModule.OctaveCount; }
			set
			{
				xDistortModule.OctaveCount = value;
				yDistortModule.OctaveCount = value;
				zDistortModule.OctaveCount = value;
			}
		}

		public double Frequency
		{
			get { return xDistortModule.Frequency; }
			set
			{
				xDistortModule.Frequency = value;
				yDistortModule.Frequency = value;
				zDistortModule.Frequency = value;
			}
		}

		public int Seed
		{
			get { return xDistortModule.Seed; }
			set
			{
				xDistortModule.Seed = (value);
				yDistortModule.Seed = (value + 1);
				zDistortModule.Seed = (value + 2);
			}
		}

		public void GetValue(double x, double y, double z, out double outX, out double outY, out double outZ)
		{
			// Get the values from the three noise::module::Perlin noise modules and
			// add each value to each coordinate of the input value.  There are also
			// some offsets added to the coordinates of the input values.  This prevents
			// the distortion modules from returning zero if the (x, y, z) coordinates,
			// when multiplied by the frequency, are near an integer boundary.  This is
			// due to a property of gradient coherent noise, which returns zero at
			// integer boundaries.
			var x0 = x + (12414.0 / 65536.0);
			var y0 = y + (65124.0 / 65536.0);
			var z0 = z + (31337.0 / 65536.0);
			var x1 = x + (26519.0 / 65536.0);
			var y1 = y + (18128.0 / 65536.0);
			var z1 = z + (60493.0 / 65536.0);
			var x2 = x + (53820.0 / 65536.0);
			var y2 = y + (11213.0 / 65536.0);
			var z2 = z + (44845.0 / 65536.0);
			outX = x + (xDistortModule.GetValue(x0, y0, z0) * Power);
			outY = y + (yDistortModule.GetValue(x1, y1, z1) * Power);
			outZ = z + (zDistortModule.GetValue(x2, y2, z2) * Power);
		}
	}
}
