namespace Noise.Modules
{
	public class Select : Module
	{
		public const double DefaultSelectEdgeFalloff = 0.0;
		public const double DefaultSelectLowerBound = -1.0;
		public const double DefaultSelectUpperBound = 1.0;

		private double backEdgeFalloff;

		public Select()
		{
			LowerBound = DefaultSelectLowerBound;
			UpperBound = DefaultSelectUpperBound;
			backEdgeFalloff = DefaultSelectEdgeFalloff;
		}

		public Select(double lowerBound, double upperBound)
		{
			SetBounds(lowerBound, upperBound);
			EdgeFalloff = DefaultSelectEdgeFalloff;
		}

		public Select(double lowerBound, double upperBound, double edgeFalloff)
		{
			SetBounds(lowerBound, upperBound);
			EdgeFalloff = edgeFalloff;
		}

		public Select(Module sourceModule1, Module sourceModule2, Module controlModule)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
			ControlModule = controlModule;
			LowerBound = DefaultSelectLowerBound;
			UpperBound = DefaultSelectUpperBound;
			backEdgeFalloff = DefaultSelectEdgeFalloff;
		}

		public Select(Module sourceModule1, Module sourceModule2, Module controlModule, double lowerBound, double upperBound)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
			ControlModule = controlModule;
			SetBounds(lowerBound, upperBound);
			EdgeFalloff = DefaultSelectEdgeFalloff;
		}

		public Select(Module sourceModule1, Module sourceModule2, Module controlModule, double lowerBound, double upperBound, double edgeFalloff)
		{
			SourceModule1 = sourceModule1;
			SourceModule2 = sourceModule2;
			ControlModule = controlModule;
			SetBounds(lowerBound, upperBound);
			EdgeFalloff = edgeFalloff;
		}

		public double EdgeFalloff
		{
			get { return backEdgeFalloff; }
			set
			{
				// Make sure that the edge falloff curves do not overlap.
				var boundSize = UpperBound - LowerBound;
				backEdgeFalloff = (value > boundSize / 2) ? boundSize / 2 : value;
			}
		}

		public double LowerBound { get; private set; }
		public double UpperBound { get; private set; }

		public Module SourceModule1 { get; set; }
		public Module SourceModule2 { get; set; }
		public Module ControlModule { get; set; }

		public void SetBounds(double lowerBound, double upperBound)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;

			// Make sure that the edge falloff curves do not overlap.
			EdgeFalloff = EdgeFalloff;
		}

		public override double this[double x, double y, double z]
		{
			get
			{
				var controlValue = ControlModule[x, y, z];
				if(EdgeFalloff > 0.0)
				{
					if(controlValue < (LowerBound - EdgeFalloff))
					{
						// The output value from the control module is below the selector
						// threshold; return the output value from the first source module.
						return SourceModule1[x, y, z];
					}
					if(controlValue < (LowerBound + EdgeFalloff))
					{
						// The output value from the control module is near the lower end of the
						// selector threshold and within the smooth curve. Interpolate between
						// the output values from the first and second source modules.
						var lowerCurve = (LowerBound - EdgeFalloff);
						var upperCurve = (LowerBound + EdgeFalloff);
						var alpha = Interp.SCurve3((controlValue - lowerCurve) / (upperCurve - lowerCurve));
						return Interp.LinearInterp(SourceModule1[x, y, z], SourceModule2[x, y, z], alpha);
					}
					if(controlValue < (UpperBound - EdgeFalloff))
					{
						// The output value from the control module is within the selector
						// threshold; return the output value from the second source module.
						return SourceModule2[x, y, z];
					}
					if(controlValue < (UpperBound + EdgeFalloff))
					{
						// The output value from the control module is near the upper end of the
						// selector threshold and within the smooth curve. Interpolate between
						// the output values from the first and second source modules.
						var lowerCurve = (UpperBound - EdgeFalloff);
						var upperCurve = (UpperBound + EdgeFalloff);
						var alpha = Interp.SCurve3((controlValue - lowerCurve) / (upperCurve - lowerCurve));
						return Interp.LinearInterp(SourceModule2[x, y, z], SourceModule1[x, y, z], alpha);
					}
					// Output value from the control module is above the selector threshold;
					// return the output value from the first source module.
					return SourceModule1[x, y, z];
				}
				if(controlValue < LowerBound || controlValue > UpperBound)
				{
					return SourceModule1[x, y, z];
				}
				return SourceModule2[x, y, z];
			}
		}
	}
}