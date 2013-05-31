using Noise.Modules;

namespace Noise.Models
{
	public class Plane
	{
		public Plane() {}

		public Plane(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public Module ConnectedModule { get; set; }

		public double GetValue(double x, double z)
		{
			return ConnectedModule.GetValue(x, 0, z);
		}
	}
}
