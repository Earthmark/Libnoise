namespace Noise.Modules
{
	public class Select : Module
	{
		private const double DefaultSelectEdgeFalloff = 0.0;
		private const double DefaultSelectLowerBound = -1.0;
		private const double DefaultSelectUpperBound = 1.0;

		private double edgeFalloff;

		public double EdgeFalloff
		{
			get { return edgeFalloff; }
			set
			{
				// Make sure that the edge falloff curves do not overlap.
				var boundSize = UpperBound - LowerBound;
				edgeFalloff = (value > boundSize / 2) ? boundSize / 2 : value;
			}
		}

		public double LowerBound { get; private set; }
		public double UpperBound { get; private set; }

		public Module SourceModule1 { get; set; }
		public Module SourceModule2 { get; set; }
		public Module ControlModule { get; set; }

		public Select(Module sourceModule1 = null, Module sourceModule2 = null, Module controlModule = null,
			double lowerBound = DefaultSelectLowerBound, double upperBound = DefaultSelectUpperBound,
			double edgeFalloff = DefaultSelectEdgeFalloff)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
			ControlModule = controlModule;
			this.edgeFalloff = edgeFalloff;
			SetBounds(lowerBound, upperBound);
		}

		private void SetBounds(double lowerBound, double upperBound)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;

			// Make sure that the edge falloff curves do not overlap.
			EdgeFalloff = EdgeFalloff;
		}

		public override double GetValue(double x, double y, double z)
		{

			var controlValue = ControlModule.GetValue(x, y, z);
			if(EdgeFalloff > 0.0)
			{
				if(controlValue < (LowerBound - EdgeFalloff))
				{
					// The output value from the control module is below the selector
					// threshold; return the output value from the first source module.
					return SourceModule1.GetValue(x, y, z);
				}
				double alpha;
				if(controlValue < (LowerBound + EdgeFalloff))
				{
					// The output value from the control module is near the lower end of the
					// selector threshold and within the smooth curve. Interpolate between
					// the output values from the first and second source modules.
					var lowerCurve = (LowerBound - EdgeFalloff);
					var upperCurve = (LowerBound + EdgeFalloff);
					alpha = Interp.SCurve3(
						(controlValue - lowerCurve) / (upperCurve - lowerCurve));
					return Interp.LinearInterp(
						SourceModule1.GetValue(x, y, z), SourceModule2.GetValue(x, y, z),
						alpha);
				}
				if(controlValue < (UpperBound - EdgeFalloff))
				{
					// The output value from the control module is within the selector
					// threshold; return the output value from the second source module.
					return SourceModule2.GetValue(x, y, z);
				}
				if(controlValue < (UpperBound + EdgeFalloff))
				{
					// The output value from the control module is near the upper end of the
					// selector threshold and within the smooth curve. Interpolate between
					// the output values from the first and second source modules.
					var lowerCurve = (UpperBound - EdgeFalloff);
					var upperCurve = (UpperBound + EdgeFalloff);
					alpha = Interp.SCurve3(
						(controlValue - lowerCurve) / (upperCurve - lowerCurve));
					return Interp.LinearInterp(
						SourceModule2.GetValue(x, y, z), SourceModule1.GetValue(x, y, z),
						alpha);
				}
				// Output value from the control module is above the selector threshold;
				// return the output value from the first source module.
				return SourceModule1.GetValue(x, y, z);
			}
			if(controlValue < LowerBound || controlValue > UpperBound)
			{
				return SourceModule1.GetValue(x, y, z);
			}
			return SourceModule2.GetValue(x, y, z);
		}
	}
}
