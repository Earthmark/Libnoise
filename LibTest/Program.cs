using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibNoise;
using LibNoise.Modfiers;
using LibNoise.Modifiers;
using Billow = LibNoise.Billow;
using Math = System.Math;

namespace LibTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var noise = GetTestModule();
			var noise2 = GetTestModule2();

			var random = new Random();
			for(var i = 0; i < 10000; i++)
			{
				var x = (random.NextDouble() - 0.5) * 100000;
				var y = (random.NextDouble() - 0.5) * 100000;
				var z = (random.NextDouble() - 0.5) * 100000;
				var val1 = noise.GetValue(x, y, z);
				var val2 = noise2.GetValue(x, y, z);

				if (Math.Abs(val1 - val2) > 0.0001)
					Console.WriteLine("error");
			}
			Console.WriteLine("Done");
		}

		private static IModule GetTestModule2()
		{
			var baseFlatTerrain = new Billow();

			var flatTerrain = new ScaleBiasOutput(baseFlatTerrain);

			var mountainTerrain = new RidgedMultifractal();

			var terrainType = new Voronoi();

			var mod = new Terrace(terrainType);
			mod.ControlPoints.Add(3);
			mod.ControlPoints.Add(22);
			mod.GetValue(0, 0, 0);
			return mod;
		}

		private static Noise.Modules.Module GetTestModule()
		{
			var baseFlatTerrain = new Noise.Modules.Billow();

			var flatTerrain = new Noise.Modules.ScaleBias(baseFlatTerrain);

			var mountainTerrain = new Noise.Modules.RidgedMulti();

			var terrainType = new Noise.Modules.Voronoi();

			var mod = new Noise.Modules.Terrace(terrainType);
			mod.AddControlPoint(3);
			mod.AddControlPoint(22);
			return mod;
		}
	}
}
