﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noise.Modules;

namespace Noise.Models
{
	public class Sphere
	{
		public Module ConnectedModule { get; set; }

		public Sphere() {}

		public Sphere(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public double GetValue(double lat, double lon)
		{
			double x, y, z;
			Latlon.LatLonToXYZ(lat, lon, out x, out y, out z);
			return ConnectedModule.GetValue(x, y, z);
		}
	}
}
