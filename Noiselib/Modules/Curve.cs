using System.Collections.Generic;

namespace Noiselib.Modules
{
	public struct ControlPoint
	{
		public ControlPoint(double input, double output)
			: this()
		{
			InputValue = input;
			OutputValue = output;
		}

		public double InputValue { get; set; }
		public double OutputValue { get; set; }
	}

	public class Curve : Module
	{
		private readonly SortedList<double, double> controlPoints;

		public Curve()
		{
			controlPoints = new SortedList<double, double>();
		}

		public Curve(Module connectedModule, params ControlPoint[] points)
		{
			controlPoints = new SortedList<double, double>();
			foreach(ControlPoint point in points)
			{
				AddControlPoint(point);
			}

			ConnectedModule = connectedModule;
		}

		public Curve(Module connectedModule, IEnumerable<ControlPoint> points)
		{
			controlPoints = new SortedList<double, double>();
			foreach(ControlPoint point in points)
			{
				AddControlPoint(point);
			}

			ConnectedModule = connectedModule;
		}

		public Curve(params ControlPoint[] points)
		{
			controlPoints = new SortedList<double, double>();
			foreach(ControlPoint point in points)
			{
				AddControlPoint(point);
			}
		}

		public Curve(IEnumerable<ControlPoint> points)
		{
			controlPoints = new SortedList<double, double>();
			foreach(ControlPoint point in points)
			{
				AddControlPoint(point);
			}
		}

		public Module ConnectedModule { get; set; }

		public SortedList<double, double> ControlPoints
		{
			get { return controlPoints; }
		}

		public override double this[double x, double y, double z]
		{
			get
			{
				// Get the output value from the source module.
				double sourceModuleValue = ConnectedModule[x, y, z];

				IList<double> inputs = controlPoints.Keys;
				IList<double> outputs = controlPoints.Values;

				// Find the first element in the control point array that has an input value
				// larger than the output value from the source module.
				int indexPos;
				for(indexPos = 0; indexPos < controlPoints.Count; indexPos++)
				{
					if(sourceModuleValue < inputs[indexPos])
					{
						break;
					}
				}

				// Find the four nearest control points so that we can perform cubic
				// interpolation.
				int index0 = Misc.ClampValue(indexPos - 2, 0, controlPoints.Count - 1);
				int index1 = Misc.ClampValue(indexPos - 1, 0, controlPoints.Count - 1);
				int index2 = Misc.ClampValue(indexPos, 0, controlPoints.Count - 1);
				int index3 = Misc.ClampValue(indexPos + 1, 0, controlPoints.Count - 1);

				// If some control points are missing (which occurs if the value from the
				// source module is greater than the largest input value or less than the
				// smallest input value of the control point array), get the corresponding
				// output value of the nearest control point and exit now.
				if(index1 == index2)
				{
					return outputs[index1];
				}

				// Compute the alpha value used for cubic interpolation.
				double input0 = inputs[index1];
				double input1 = inputs[index2];
				double alpha = (sourceModuleValue - input0) / (input1 - input0);

				// Now perform the cubic interpolation given the alpha value.
				return Interp.CubicInterp(
					outputs[index0], outputs[index1], outputs[index2], outputs[index3],
					alpha);
			}
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x, double y]
		{
			get
			{
				// Get the output value from the source module.
				double sourceModuleValue = ConnectedModule[x, y];

				IList<double> inputs = controlPoints.Keys;
				IList<double> outputs = controlPoints.Values;

				// Find the first element in the control point array that has an input value
				// larger than the output value from the source module.
				int indexPos;
				for(indexPos = 0; indexPos < controlPoints.Count; indexPos++)
				{
					if(sourceModuleValue < inputs[indexPos])
					{
						break;
					}
				}

				// Find the four nearest control points so that we can perform cubic
				// interpolation.
				int index0 = Misc.ClampValue(indexPos - 2, 0, controlPoints.Count - 1);
				int index1 = Misc.ClampValue(indexPos - 1, 0, controlPoints.Count - 1);
				int index2 = Misc.ClampValue(indexPos, 0, controlPoints.Count - 1);
				int index3 = Misc.ClampValue(indexPos + 1, 0, controlPoints.Count - 1);

				// If some control points are missing (which occurs if the value from the
				// source module is greater than the largest input value or less than the
				// smallest input value of the control point array), get the corresponding
				// output value of the nearest control point and exit now.
				if(index1 == index2)
				{
					return outputs[index1];
				}

				// Compute the alpha value used for cubic interpolation.
				double input0 = inputs[index1];
				double input1 = inputs[index2];
				double alpha = (sourceModuleValue - input0) / (input1 - input0);

				// Now perform the cubic interpolation given the alpha value.
				return Interp.CubicInterp(
					outputs[index0], outputs[index1], outputs[index2], outputs[index3],
					alpha);
			}
		}

		public void AddControlPoint(ControlPoint point)
		{
			controlPoints[point.InputValue] = point.OutputValue;
		}
	}
}