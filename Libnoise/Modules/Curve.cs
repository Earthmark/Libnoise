using System.Collections.Generic;

namespace Noise.Modules
{
	public struct ControlPoint
	{
		public double InputValue { get; set; }
		public double OutputValue { get; set; }

		public ControlPoint(double input, double output)
			: this()
		{
			InputValue = input;
			OutputValue = output;
		}
	}

	public class Curve : Module
	{
		private readonly SortedList<double, double> controlPoints;

		public Module ConnectedModule { get; set; }

		public SortedList<double, double> ControlPoints
		{
			get { return controlPoints; }
		}

		public Curve()
		{
			controlPoints = new SortedList<double, double>();
		}

		public Curve(Module connectedModule, params ControlPoint[] points)
		{
			controlPoints = new SortedList<double, double>();
			foreach (var point in points)
				AddControlPoint(point);

			ConnectedModule = connectedModule;
		}

		public Curve(Module connectedModule, IEnumerable<ControlPoint> points)
		{
			controlPoints = new SortedList<double, double>();
			foreach (var point in points)
				AddControlPoint(point);

			ConnectedModule = connectedModule;
		}

		public Curve(params ControlPoint[] points)
		{
			controlPoints = new SortedList<double, double>();
			foreach (var point in points)
				AddControlPoint(point);
		}

		public Curve(IEnumerable<ControlPoint> points)
		{
			controlPoints = new SortedList<double, double>();
			foreach (var point in points)
				AddControlPoint(point);
		}

		public void AddControlPoint(ControlPoint point)
		{
			controlPoints[point.InputValue] = point.OutputValue;
		}

		public override double GetValue(double x, double y, double z)
		{
			// Get the output value from the source module.
			var sourceModuleValue = ConnectedModule.GetValue(x, y, z);

			var inputs = controlPoints.Keys;
			var outputs = controlPoints.Values;

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
			var index0 = Misc.ClampValue(indexPos - 2, 0, controlPoints.Count - 1);
			var index1 = Misc.ClampValue(indexPos - 1, 0, controlPoints.Count - 1);
			var index2 = Misc.ClampValue(indexPos, 0, controlPoints.Count - 1);
			var index3 = Misc.ClampValue(indexPos + 1, 0, controlPoints.Count - 1);

			// If some control points are missing (which occurs if the value from the
			// source module is greater than the largest input value or less than the
			// smallest input value of the control point array), get the corresponding
			// output value of the nearest control point and exit now.
			if(index1 == index2)
			{
				return outputs[index1];
			}

			// Compute the alpha value used for cubic interpolation.
			var input0 = inputs[index1];
			var input1 = inputs[index2];
			var alpha = (sourceModuleValue - input0) / (input1 - input0);

			// Now perform the cubic interpolation given the alpha value.
			return Interp.CubicInterp(
				outputs[index0], outputs[index1], outputs[index2], outputs[index3],
				alpha);
		}
	}
}
