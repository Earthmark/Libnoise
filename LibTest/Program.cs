using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibNoise;
using LibNoise.Modifiers;
using Noise.Modules;
using Billow = LibNoise.Billow;
using Math = System.Math;
using Perlin = Noise.Modules.Perlin;
using Select = LibNoise.Modifiers.Select;

namespace LibTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Module noise = GetTestModule();
			IModule noise2 = GetTestModule2();

			noise = new ScaleBias(noise, 0.5, 0.5);
			noise2 = new ScaleBiasOutput(noise2) {Bias = 0.5, Scale = 0.5};

			var random = new Random();
			for(var i = 0; i < 10000; i++)
			{
				var x = (random.NextDouble() - 0.5) * 100000;
				var y = (random.NextDouble() - 0.5) * 100000;
				var z = (random.NextDouble() - 0.5) * 100000;
				var val1 = noise.GetValue(x, y, z);
				var val2 = noise2.GetValue(x, y, z);

				Console.Write(Math.Abs(val1 - val2) < 0.0001);
			}
		}

		public static IModule GetTestModule2()
		{
			var baseFlatTerrain = new Billow
			{
				Frequency = 5.0f
			};

			var flatTerrain = new ScaleBiasOutput(baseFlatTerrain)
			{
				Scale = 16.0f,
				Bias = -0.25f
			};

			var mountainTerrain = new RidgedMultifractal();

			var terrainType = new LibNoise.Perlin
			{
				Persistence = 0.25f,
				Frequency = 0.015f,
				Lacunarity = 1.25f,
				OctaveCount = 8,
				NoiseQuality = NoiseQuality.Standard,
				Seed = 0
			};
			
			//noise = new ScaleOutput(terrainType, Amplitude);
			//noise = new Add(new Constant(TerrainGenerator.MinimumGroundHeight), noise);

			var ter = new Select(terrainType, flatTerrain, mountainTerrain)
			{
				EdgeFalloff = 0.525f
			};
			ter.SetBounds(0.0, 1000.0);

			return ter;
		}

		public static Module GetTestModule()
		{
			var baseFlatTerrain = new Noise.Modules.Billow
			{
				Frequency = 5.0f
			};

			var flatTerrain = new ScaleBias(baseFlatTerrain)
			{
				Scale = 16.0f,
				Bias = -0.25f
			};

			var mountainTerrain = new RidgedMulti();

			var terrainType = new Perlin
			{
				Persistence = 0.25f,
				Frequency = 0.015f,
				Lacunarity = 1.25f,
				OctaveCount = 8,
				NoiseQuality = Noise.NoiseQuality.Standard,
				Seed = 0
			};

			//noise = new ScaleOutput(terrainType, Amplitude);
			//noise = new Add(new Constant(TerrainGenerator.MinimumGroundHeight), noise);

			var ter = new Noise.Modules.Select(terrainType, flatTerrain, mountainTerrain)
			{
				EdgeFalloff = 0.525f
			};
			ter.SetBounds(0.0, 1000.0);

			return ter;
		}
	}
}
