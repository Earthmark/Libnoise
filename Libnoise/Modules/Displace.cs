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

		public override double GetValue(double x, double y, double z)
		{
			// Get the output values from the three displacement modules.  Add each
			// value to the corresponding coordinate in the input value.
			double xDisplace = x + (DisplaceModuleX.GetValue(x, y, z));
			double yDisplace = y + (DisplaceModuleY.GetValue(x, y, z));
			double zDisplace = z + (DisplaceModuleZ.GetValue(x, y, z));

			// Retrieve the output value using the offsetted input value instead of
			// the original input value.
			return SourceModule.GetValue(xDisplace, yDisplace, zDisplace);
		}
	}
}