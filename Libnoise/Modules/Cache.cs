using System;

namespace Noise.Modules
{
	public class Cache : Module
	{
		private const double Epsilon = 0.0000001;
		protected double cachedValue;

		private Module sourceModule;

		protected bool isCached;

		protected double xCache;
		protected double yCache;
		protected double zCache;

		public Cache() {}

		public Cache(Module sourceModule)
		{
			SourceModule = sourceModule;
		}

		public Module SourceModule
		{
			get { return sourceModule; }
			set
			{
				sourceModule = value;
				isCached = false;
			}
		}

		public override double GetValue(double x, double y, double z)
		{
			if(!(isCached && Math.Abs(x - xCache) < Epsilon && Math.Abs(y - yCache) < Epsilon && Math.Abs(z - zCache) < Epsilon))
			{
				cachedValue = SourceModule.GetValue(x, y, z);
				xCache = x;
				yCache = y;
				zCache = z;
			}
			isCached = true;
			return cachedValue;
		}
	}
}
