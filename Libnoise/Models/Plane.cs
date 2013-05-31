using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noise.Modules;

namespace Noise.Models
{
	public class Plane
	{
		public Module ConnectedModule { get; set; }

		public Plane() {}

		public Plane(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public double GetValue(double x, double z)
		{
			return ConnectedModule.GetValue(x, 0, z);
		}
	}
}
