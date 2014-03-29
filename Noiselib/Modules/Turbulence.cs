namespace Noiselib.Modules
{
	public class Turbulence : Module
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

		public Turbulence(Module sourceModule)
			: this()
		{
			SourceModule = sourceModule;
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

		public Turbulence(Module sourceModule, double power, double frequency, int roughness, int seed)
		{
			SourceModule = sourceModule;
			zDistortModule = new Perlin();
			yDistortModule = new Perlin();
			xDistortModule = new Perlin();
			Power = power;
			Seed = seed;
			Frequency = frequency;
			Roughness = roughness;
		}

		public Module SourceModule { get; set; }

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

		public override double this[double x, double y, double z]
		{
			get
			{
				// Get the values from the three noise::module::Perlin noise modules and
				// add each value to each coordinate of the input value.  There are also
				// some offsets added to the coordinates of the input values.  This prevents
				// the distortion modules from returning zero if the (x, y, z) coordinates,
				// when multiplied by the frequency, are near an integer boundary.  This is
				// due to a property of gradient coherent noise, which returns zero at
				// integer boundaries.
				double x0 = x + (12414.0 / 65536.0);
				double y0 = y + (65124.0 / 65536.0);
				double z0 = z + (31337.0 / 65536.0);
				double x1 = x + (26519.0 / 65536.0);
				double y1 = y + (18128.0 / 65536.0);
				double z1 = z + (60493.0 / 65536.0);
				double x2 = x + (53820.0 / 65536.0);
				double y2 = y + (11213.0 / 65536.0);
				double z2 = z + (44845.0 / 65536.0);
				double xDistort = x + (xDistortModule[x0, y0, z0] * Power);
				double yDistort = y + (yDistortModule[x1, y1, z1] * Power);
				double zDistort = z + (zDistortModule[x2, y2, z2] * Power);

				// Retrieve the output value at the offsetted input value instead of the
				// original input value.
				return SourceModule[xDistort, yDistort, zDistort];
			}
		}

		public override double this[double x, double y]
		{
			get
			{
				// Get the values from the three noise::module::Perlin noise modules and
				// add each value to each coordinate of the input value.  There are also
				// some offsets added to the coordinates of the input values.  This prevents
				// the distortion modules from returning zero if the (x, y, z) coordinates,
				// when multiplied by the frequency, are near an integer boundary.  This is
				// due to a property of gradient coherent noise, which returns zero at
				// integer boundaries.
				double x0 = x + (12414.0 / 65536.0);
				double y0 = y + (65124.0 / 65536.0);
				double x1 = x + (26519.0 / 65536.0);
				double y1 = y + (18128.0 / 65536.0);
				double xDistort = x + (xDistortModule[x0, y0] * Power);
				double yDistort = y + (yDistortModule[x1, y1] * Power);

				// Retrieve the output value at the offsetted input value instead of the
				// original input value.
				return SourceModule[xDistort, yDistort];
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
				// Get the values from the three noise::module::Perlin noise modules and
				// add each value to each coordinate of the input value.  There are also
				// some offsets added to the coordinates of the input values.  This prevents
				// the distortion modules from returning zero if the (x, y, z) coordinates,
				// when multiplied by the frequency, are near an integer boundary.  This is
				// due to a property of gradient coherent noise, which returns zero at
				// integer boundaries.
				double x0 = x + (12414.0 / 65536.0);
				double xDistort = x + (xDistortModule[x0] * Power);

				// Retrieve the output value at the offsetted input value instead of the
				// original input value.
				return SourceModule[xDistort];
			}
		}
	}
}