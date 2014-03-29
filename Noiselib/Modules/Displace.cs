namespace Noiselib.Modules
{
	public class Displace : Module
	{
		public Displace() {}

		public Displace(Module sourceModule)
		{
			SourceModule = sourceModule;
		}

		public Displace(Module displaceModuleX, Module displaceModuleY, Module displaceModuleZ)
		{
			DisplaceModuleX = displaceModuleX;
			DisplaceModuleY = displaceModuleY;
			DisplaceModuleZ = displaceModuleZ;
		}

		public Displace(Module sourceModule, Module displaceModuleX, Module displaceModuleY, Module displaceModuleZ)
		{
			SourceModule = sourceModule;
			DisplaceModuleX = displaceModuleX;
			DisplaceModuleY = displaceModuleY;
			DisplaceModuleZ = displaceModuleZ;
		}

		public Module DisplaceModuleX { get; set; }
		public Module DisplaceModuleY { get; set; }
		public Module DisplaceModuleZ { get; set; }
		public Module SourceModule { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				// Get the output values from the three displacement modules.  Add each
				// value to the corresponding coordinate in the input value.
				double xDisplace = x + (DisplaceModuleX[x, y, z]);
				double yDisplace = y + (DisplaceModuleY[x, y, z]);
				double zDisplace = z + (DisplaceModuleZ[x, y, z]);

				// Retrieve the output value using the offsetted input value instead of
				// the original input value.
				return SourceModule[xDisplace, yDisplace, zDisplace];
			}
		}

		public override double this[double x, double y]
		{
			get
			{
				// Get the output values from the three displacement modules.  Add each
				// value to the corresponding coordinate in the input value.
				double xDisplace = x + (DisplaceModuleX[x, y]);
				double yDisplace = y + (DisplaceModuleY[x, y]);

				// Retrieve the output value using the offsetted input value instead of
				// the original input value.
				return SourceModule[xDisplace, yDisplace];
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
				// Get the output values from the three displacement modules.  Add each
				// value to the corresponding coordinate in the input value.
				double xDisplace = x + (DisplaceModuleX[x]);

				// Retrieve the output value using the offsetted input value instead of
				// the original input value.
				return SourceModule[xDisplace];
			}
		}

		public void SetDisplaceModules(Module displaceModuleX, Module displaceModuleY, Module displaceModuleZ)
		{
			DisplaceModuleX = displaceModuleX;
			DisplaceModuleY = displaceModuleY;
			DisplaceModuleZ = displaceModuleZ;
		}
	}
}