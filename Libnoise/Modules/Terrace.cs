using System;
using System.Collections.Generic;

namespace Noise.Modules
{
	public class Terrace : Module
	{
		private double[] controlPoints;

		public Terrace()
		{
			controlPoints = new double[0];
		}

		public Terrace(Module sourceModule)
		{
			controlPoints = new double[0];
			SourceModule = sourceModule;
		}

		public Terrace(IEnumerable<double> points)
		{
			controlPoints = new double[0];
			foreach (var point in points)
				AddControlPoint(point);
		}

		public Terrace(Module sourceModule, IEnumerable<double> points)
		{
			controlPoints = new double[0];
			SourceModule = sourceModule;
			foreach (var point in points)
				AddControlPoint(point);
		}

		private bool InvertTerraces { get; set; }

		public Module SourceModule { get; set; }

		public void AddControlPoint(double value)
		{
			// Find the insertion point for the new control point and insert the new
			// point at that position.  The control point array will remain sorted by
			// value.
			int insertionPos = FindInsertionPos(value);
			InsertAtPos(insertionPos, value);
		}

		public void ClearAllControlPoints()
		{
			controlPoints = null;
		}

		public int FindInsertionPos(double value)
		{
			int insertionPos;
			for(insertionPos = 0; insertionPos < controlPoints.Length; insertionPos++)
			{
				if(value < controlPoints[insertionPos])
				{
					// We found the array index in which to insert the new control point.
					// Exit now.
					break;
				}
			}
			return insertionPos;
		}

		public override double GetValue(double x, double y, double z)
		{
			// Get the output value from the source module.
			double sourceModuleValue = SourceModule.GetValue(x, y, z);

			// Find the first element in the control point array that has a value
			// larger than the output value from the source module.
			int indexPos;
			for (indexPos = 0; indexPos < controlPoints.Length; indexPos++)
			{
				if (sourceModuleValue < controlPoints[indexPos])
				{
					break;
				}
			}

			// Find the two nearest control points so that we can map their values
			// onto a quadratic curve.
			int index0 = Misc.ClampValue(indexPos - 1, 0, controlPoints.Length - 1);
			int index1 = Misc.ClampValue(indexPos, 0, controlPoints.Length - 1);

			// If some control points are missing (which occurs if the output value from
			// the source module is greater than the largest value or less than the
			// smallest value of the control point array), get the value of the nearest
			// control point and exit now.
			if(index0 == index1)
			{
				return controlPoints[index1];
			}

			// Compute the alpha value used for linear interpolation.
			double value0 = controlPoints[index0];
			double value1 = controlPoints[index1];
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

		public void InsertAtPos(int insertionPos, double value)
		{
			// Make room for the new control point at the specified position within
			// the control point array.  The position is determined by the value of
			// the control point; the control points must be sorted by value within
			// that array.
			double[] newControlPoints = new double[controlPoints.Length + 1];
			for (int i = 0; i < controlPoints.Length - 1; i++)
			{
				if(i < insertionPos)
				{
					newControlPoints[i] = controlPoints[i];
				}
				else
				{
					newControlPoints[i + 1] = controlPoints[i];
				}
			}
			controlPoints = newControlPoints;

			// Now that we've made room for the new control point within the array,
			// add the new control point.
			controlPoints[insertionPos] = value;
		}

		public void MakeControlPoints(int controlPointCount)
		{
			if(controlPointCount < 2)
			{
				throw new InvalidOperationException("Not enough control points.");
			}

			ClearAllControlPoints();

			double terraceStep = 2.0 / ((double) controlPointCount - 1.0);
			double curValue = -1.0;
			for(int i = 0; i < (int) controlPointCount; i++)
			{
				AddControlPoint(curValue);
				curValue += terraceStep;
			}
		}
	}
}
