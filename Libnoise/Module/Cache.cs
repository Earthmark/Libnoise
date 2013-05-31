using System;

namespace Noise.Module
{
	public class Cache : Module
	{
		private const double Epsilon = 0.0000001;

		protected bool isCached;

		protected double xCache;
		protected double yCache;
		protected double zCache;

		protected double cachedValue;

		private Module connectedModule;

		public Module ConnectedModule
		{
			get { return connectedModule; }
			set
			{
				connectedModule = value;
				isCached = false;
			}
		}

		public Cache()
		{
			
		}

		public Cache(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public override double GetValue(double x, double y, double z)
		{
			if (!(isCached && Math.Abs(x - xCache) < Epsilon && Math.Abs(y - yCache) < Epsilon && Math.Abs(z - zCache) < Epsilon))
			{
				cachedValue = ConnectedModule.GetValue(x, y, z);
				xCache = x;
				yCache = y;
				zCache = z;
			}
			isCached = true;
			return cachedValue;
		}
	}
}
