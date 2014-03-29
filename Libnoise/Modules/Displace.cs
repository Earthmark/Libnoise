﻿namespace Noise.Modules
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

		public void SetDisplaceModules(Module displaceModuleX, Module displaceModuleY, Module displaceModuleZ)
		{
			DisplaceModuleX = displaceModuleX;
			DisplaceModuleY = displaceModuleY;
			DisplaceModuleZ = displaceModuleZ;
		}

		public override double this[double x, double y, double z]
		{
			get
			{
				// Get the output values from the three displacement modules.  Add each
				// value to the corresponding coordinate in the input value.
				var xDisplace = x + (DisplaceModuleX[x, y, z]);
				var yDisplace = y + (DisplaceModuleY[x, y, z]);
				var zDisplace = z + (DisplaceModuleZ[x, y, z]);

				// Retrieve the output value using the offsetted input value instead of
				// the original input value.
				return SourceModule[xDisplace, yDisplace, zDisplace];
			}
		}
	}
}
