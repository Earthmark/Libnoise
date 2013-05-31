﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Module
{
	public class Perlin : Module
	{
		private int octaveCount;

		/// Default frequency for the noise::module::Perlin noise module.
		public const double DefaultPerlinFrequency = 1.0;

		/// Default lacunarity for the noise::module::Perlin noise module.
		public const double DefaultPerlinLacunarity = 2.0;

		/// Default number of octaves for the noise::module::Perlin noise module.
		public const int DefaultPerlinOctaveCount = 6;

		/// Default persistence value for the noise::module::Perlin noise module.
		public const double DefaultPerlinPersistence = 0.5;

		/// Default noise quality for the noise::module::Perlin noise module.
		public const NoiseQuality DefaultPerlinQuality = NoiseQuality.Standard;

		/// Default noise seed for the noise::module::Perlin noise module.
		public const int DefaultPerlinSeed = 0;

		/// Maximum number of octaves for the noise::module::Perlin noise module.
		public const int PerlinMaxOctave = 30;

		/// Frequency of the first octave.
		public double Frequency { get; set; }

		/// Frequency multiplier between successive octaves.
		public double Lacunarity { get; set; }

		/// Quality of the Perlin noise.
		public NoiseQuality NoiseQuality { get; set; }

		/// Total number of octaves that generate the Perlin noise.
		public int OctaveCount
		{
			get { return octaveCount; }
			set
			{
				if(octaveCount < 1 || octaveCount > PerlinMaxOctave)
				{
					throw new ArgumentException("Count was too high, above " + PerlinMaxOctave, "value");
				}
				octaveCount = value;
			}
		}

		/// Persistence of the Perlin noise.
		public double Persistence { get; set; }

		/// Seed value used by the Perlin-noise function.
		public int Seed { get; set; }

		public Perlin()
		{
			Seed = DefaultPerlinSeed;
			Persistence = DefaultPerlinPersistence;
			OctaveCount = DefaultPerlinOctaveCount;
			NoiseQuality = DefaultPerlinQuality;
			Lacunarity = DefaultPerlinLacunarity;
			Frequency = DefaultPerlinFrequency;
		}

		public Perlin(double frequency, double lacunarity, NoiseQuality noiseQuality, int octaveCount, double persistence, int seed)
		{
			Seed = seed;
			Persistence = persistence;
			OctaveCount = octaveCount;
			NoiseQuality = noiseQuality;
			Lacunarity = lacunarity;
			Frequency = frequency;
		}

		public override double GetValue(double x, double y, double z)
		{
			var value = 0.0;
			var curPersistence = 1.0;

			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			for(var curOctave = 0; curOctave < OctaveCount; curOctave++)
			{

				// Make sure that these floating-point values have the same range as a 32-
				// bit integer so that we can pass them to the coherent-noise functions.
				var nx = NoiseGen.MakeInt32Range(x);
				var ny = NoiseGen.MakeInt32Range(y);
				var nz = NoiseGen.MakeInt32Range(z);

				// Get the coherent-noise value from the input value and add it to the
				// final result.
				var localSeed = (Seed + curOctave) & 0x7fffffff;
				var signal = NoiseGen.GradientCoherentNoise3D(nx, ny, nz, localSeed, NoiseQuality);
				value += signal * curPersistence;

				// Prepare the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
				curPersistence *= Persistence;
			}

			return value;
		}
	}
}
