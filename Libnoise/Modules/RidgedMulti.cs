﻿using System;

namespace Noise.Modules
{
	public class RidgedMulti : Module
	{

		public const double DefaultRidgedFrequency = 1.0;

		public const double DefaultRidgedLacunarity = 2.0;

		public const int DefaultRidgedOctaveCount = 6;

		public const NoiseQuality DefaultRidgedQuality = NoiseQuality.Standard;

		public const int DefaultRidgedSeed = 0;

		public const int RidgedMaxOctave = 30;

		private readonly double[] spectralWeights;
		private double lacunarity;
		private int octaveCount;

		public RidgedMulti(double lacunarity, double frequency, NoiseQuality noiseQuality, int octaveCount, int seed)
		{
			Seed = seed;
			OctaveCount = octaveCount;
			spectralWeights = new double[RidgedMaxOctave];
			NoiseQuality = noiseQuality;
			Frequency = frequency;
			Lacunarity = lacunarity;
		}

		public RidgedMulti()
		{
			Seed = DefaultRidgedSeed;
			OctaveCount = DefaultRidgedOctaveCount;
			spectralWeights = new double[RidgedMaxOctave];
			NoiseQuality = DefaultRidgedQuality;
			Frequency = DefaultRidgedFrequency;
			Lacunarity = DefaultRidgedLacunarity;
		}

		public double Frequency { get; set; }

		public double Lacunarity
		{
			get { return lacunarity; }
			set
			{
				CalcSpectralWeights();
				lacunarity = value;
			}
		}

		public NoiseQuality NoiseQuality { get; set; }

		public int OctaveCount
		{
			get { return octaveCount; }
			set
			{
				if(value < 1 || value > RidgedMaxOctave)
					throw new ArgumentException("Count was too high, above " + RidgedMaxOctave, "value");
				octaveCount = value;
			}
		}

		public int Seed { get; set; }


		private void CalcSpectralWeights()
		{
			// This exponent parameter should be user-defined; it may be exposed in a
			// future version of libnoise.
			const double h = 1.0;

			var frequen = 1.0;
			for(var i = 0; i < RidgedMaxOctave; i++)
			{
				// Compute weight for each frequency.
				spectralWeights[i] = Math.Pow(frequen, -h);
				frequen *= Lacunarity;
			}
		}

		public override double GetValue(double x, double y, double z)
		{
			x *= Frequency;
			y *= Frequency;
			z *= Frequency;

			var value = 0.0;
			var weight = 1.0;

			// These parameters should be user-defined; they may be exposed in a
			// future version of libnoise.
			const double offset = 1.0;
			const double gain = 2.0;

			for(var curOctave = 0; curOctave < OctaveCount; curOctave++)
			{

				// Make sure that these floating-point values have the same range as a 32-
				// bit integer so that we can pass them to the coherent-noise functions.
				var nx = NoiseGen.MakeInt32Range(x);
				var ny = NoiseGen.MakeInt32Range(y);
				var nz = NoiseGen.MakeInt32Range(z);

				// Get the coherent-noise value.
				var lSeed = (Seed + curOctave) & 0x7fffffff;
				var signal = NoiseGen.GradientCoherentNoise3D(nx, ny, nz, lSeed, NoiseQuality);

				// Make the ridges.
				signal = Math.Abs(signal);
				signal = offset - signal;

				// Square the signal to increase the sharpness of the ridges.
				signal *= signal;

				// The weighting from the previous octave is applied to the signal.
				// Larger values have higher weights, producing sharp points along the
				// ridges.
				signal *= weight;

				// Weight successive contributions by the previous signal.
				weight = signal * gain;
				if(weight > 1.0)
				{
					weight = 1.0;
				}
				if(weight < 0.0)
				{
					weight = 0.0;
				}

				// Add the signal to the output value.
				value += (signal * spectralWeights[curOctave]);

				// Go to the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
			}

			return (value * 1.25) - 1.0;
		}
	}
}
