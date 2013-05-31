using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noise.Modules;

namespace Noise.Models
{
	public class Line
	{
		public bool Attenuate { get; set; }
		public Module ConnectedModule { get; set; }

		public double StartX { get; set; }
		public double StartY { get; set; }
		public double StartZ { get; set; }

		public double EndX { get; set; }
		public double EndY { get; set; }
		public double EndZ { get; set; }

		public Line()
		{
		}

		public Line(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public Line(double startX, double startY, double startZ, double endX, double endY, double endZ)
		{
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		public Line(Module connectedModule, double startX, double startY, double startZ, double endX, double endY, double endZ)
		{
			ConnectedModule = connectedModule;
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		public Line(Module connectedModule, bool attenuate)
		{
			Attenuate = attenuate;
			ConnectedModule = connectedModule;
		}

		public Line(double startX, double startY, double startZ, double endX, double endY, double endZ, bool attenuate)
		{
			Attenuate = attenuate;
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		public Line(Module connectedModule, double startX, double startY, double startZ, double endX, double endY, double endZ, bool attenuate)
		{
			Attenuate = attenuate;
			ConnectedModule = connectedModule;
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		public void SetStartPoint(double x, double y, double z)
		{
			StartX = x;
			StartY = y;
			StartZ = z;
		}

		public void SetEndPoint(double x, double y, double z)
		{
			EndX = x;
			EndY = y;
			EndZ = z;
		}

		public double GetValue(double p)
		{
			var x = (EndX - StartX) * p + StartX;
			var y = (EndY - StartY) * p + StartY;
			var z = (EndZ - StartZ) * p + StartZ;
			var value = ConnectedModule.GetValue(x, y, z);

			return Attenuate ? p * (1.0 - p) * 4 * value : value;
		}
	}
}
