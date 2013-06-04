using System;

namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs three-dimensional "billowy" noise.
	/// </summary>
	public class Billow : Module
	{
		/// <summary>
		/// Default frequency for the Noise.Module.Billow noise module.
		/// </summary>
		public const double DefaultBillowFrequency = 1.0;

		/// <summary>
		/// Default lacunarity for the the Noise.Module.Billow noise module.
		/// </summary>
		public const double DefaultBillowLacunarity = 2.0;

		/// <summary>
		/// Default number of octaves for the the Noise.Module.Billow noise module.
		/// </summary>
		public const int DefaultBillowOctaveCount = 6;

		/// <summary>
		/// Default persistence value for the the Noise.Module.Billow noise module.
		/// </summary>
		public const double DefaultBillowPersistence = 0.5;

		/// <summary>
		/// Default noise quality for the the Noise.Module.Billow noise module.
		/// </summary>
		public const NoiseQuality DefaultBillowQuality = NoiseQuality.Standard;

		/// <summary>
		/// Default noise seed for the the Noise.Module.Billow noise module.
		/// </summary>
		public const int DefaultBillowSeed = 0;

		/// <summary>
		/// Maximum number of octaves for the the Noise.Module.Billow noise module.
		/// </summary>
		public const int BillowMaxOctave = 30;

		private int octaveCount;

		/// <summary>
		/// Creates a new <see cref="Billow"/> instance using the default values.
		/// </summary>
		public Billow()
		{
			Frequency = DefaultBillowFrequency;
			Lacunarity = DefaultBillowLacunarity;
			NoiseQuality = DefaultBillowQuality;
			OctaveCount = DefaultBillowOctaveCount;
			Persistence = DefaultBillowPersistence;
			Seed = DefaultBillowSeed;
		}

		/// <summary>
		/// Creates a new <see cref="Billow"/> instance using the given values.
		/// </summary>
		/// <param name="frequency">Frequency of the first octave.</param>
		/// <param name="lacunarity">Frequency multiplier between successive octaves.</param>
		/// <param name="noiseQuality">Quality of the billowy noise.</param>
		/// <param name="octaveCount">Total number of octaves that generate the billowy noise.</param>
		/// <param name="persistence">Persistence value of the billowy noise.</param>
		/// <param name="seed">Seed value used by the billowy-noise function.</param>
		public Billow(double frequency, double lacunarity, NoiseQuality noiseQuality, int octaveCount, double persistence, int seed)
		{
			if (octaveCount < 1 || octaveCount > BillowMaxOctave)
				throw new ArgumentException("Count was too high, above " + BillowMaxOctave, "octaveCount");
			Frequency = frequency;
			Lacunarity = lacunarity;
			NoiseQuality = noiseQuality;
			OctaveCount = octaveCount;
			Persistence = persistence;
			Seed = seed;
		}

		/// <summary>
		/// Frequency of the first octave.
		/// </summary>
		public double Frequency { get; set; }

		/// <summary>
		/// Frequency multiplier between successive octaves.
		/// </summary>
		public double Lacunarity { get; set; }

		/// <summary>
		/// Quality of the billowy noise.
		/// </summary>
		public NoiseQuality NoiseQuality { get; set; }

		/// <summary>
		/// Total number of octaves that generate the billowy noise.
		/// </summary>
		public int OctaveCount
		{
			get { return octaveCount; }
			set
			{
				if (value < 1 || value > BillowMaxOctave)
					throw new ArgumentException("Count was too high, above " + BillowMaxOctave, "value"); 
				octaveCount = value;
			}
		}

		/// <summary>
		/// Persistence value of the billowy noise.
		/// </summary>
		public double Persistence { get; set; }

		/// <summary>
		/// Seed value used by the billowy-noise function.
		/// </summary>
		public int Seed { get; set; }

		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
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
				var seed = Seed + curOctave;
				var signal = NoiseGen.GradientCoherentNoise3D(nx, ny, nz, seed, NoiseQuality);
				signal = 2.0 * Math.Abs(signal) - 1.0;
				value += signal * curPersistence;

				// Prepare the next octave.
				x *= Lacunarity;
				y *= Lacunarity;
				z *= Lacunarity;
				curPersistence *= Persistence;
			}
			value += 0.5;

			return value;
		}
	}
}