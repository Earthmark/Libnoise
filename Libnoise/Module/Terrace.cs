using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noise.Module
{
	public class Terrace : Module
	{
		private readonly List<double> ControlPoints;

		private bool InvertTerraces { get; set; }

		public Module SourceModule { get; set; }

		public Terrace()
		{
			ControlPoints = new List<double>();
		}

		public Terrace(Module sourceModule)
		{
			SourceModule = sourceModule;
			ControlPoints = new List<double>();
		}

		public Terrace(IEnumerable<double> points)
		{
			ControlPoints = new List<double>(points);
			ControlPoints.Sort();
		}

		public Terrace(Module sourceModule, IEnumerable<double> points)
		{
			SourceModule = sourceModule;
			ControlPoints = new List<double>(points);
			ControlPoints.Sort();
		}

		public void AddControlPoints(params double[] points)
		{
			ControlPoints.AddRange(points);
			ControlPoints.Sort();
		}

		public void ClearAllControlPoints()
		{
			ControlPoints.Clear();
		}

		public void MakeControlPoints(int controlPointCount)
		{
			if(controlPointCount < 2)
			{
				throw new ArgumentException("must be over two control points.", "controlPointCount");
			}

			ClearAllControlPoints();

			var terraceStep = 2.0 / ( controlPointCount - 1.0);
			var curValue = -1.0;
			for(var i = 0; i < controlPointCount; i++)
			{
				AddControlPoints(curValue);
				curValue += terraceStep;
			}
		}

		public override double GetValue(double x, double y, double z)
		{
			// Get the output value from the source module.
			var sourceModuleValue = SourceModule.GetValue(x, y, z);

			// Find the first element in the control point array that has a value
			// larger than the output value from the source module.
			int indexPos;
			for (indexPos = 0; indexPos < ControlPoints.Count; indexPos++)
			{
				if (sourceModuleValue < ControlPoints[indexPos])
				{
					break;
				}
			}

			// Find the two nearest control points so that we can map their values
			// onto a quadratic curve.
			var index0 = Misc.ClampValue(indexPos - 1, 0, ControlPoints.Count - 1);
			var index1 = Misc.ClampValue(indexPos, 0, ControlPoints.Count - 1);

			// If some control points are missing (which occurs if the output value from
			// the source module is greater than the largest value or less than the
			// smallest value of the control point array), get the value of the nearest
			// control point and exit now.
			if (index0 == index1)
			{
				return ControlPoints[index1];
			}

			// Compute the alpha value used for linear interpolation.
			double value0 = ControlPoints[index0];
			double value1 = ControlPoints[index1];
			double alpha = (sourceModuleValue - value0) / (value1 - value0);
			if (InvertTerraces)
			{
				alpha = 1.0 - alpha;
				Misc.SwapValues(ref value0, ref value1);
			}

			// Squaring the alpha produces the terrace effect.
			alpha *= alpha;

			// Now perform the linear interpolation given the alpha value.
			return Interp.LinearInterp(value0, value1, alpha);
		}
	}
}
