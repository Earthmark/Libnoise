using System;

namespace Noiselib.Modules
{
	/// <summary>
	/// Defines a generic function header for linking to a noise generation method.
	/// </summary>
	/// <param name="x">The input x chord.</param>
	/// <param name="y">The input y chord.</param>
	/// <param name="z">The input z chord.</param>
	public delegate double Module(double x, double y, double z);

	public static class Generic
	{
		[Obsolete("Use Math.Abs(value) instead.")]
		public static double Abs(double value)
		{
			return Math.Abs(value);
		}

		[Obsolete("Use '+' operator.")]
		public static double Add(double value1, double value2)
		{
			return value1 + value2;
		}

		public static double Blend(double value1, double value2, double control)
		{
			var v0 = value1;
			var v1 = value2;
			var alpha = (control + 1.0) / 2.0;
			return Interp.LinearInterp(v0, v1, alpha);
		}

		[Obsolete("Implement yourself, can't be implemented here.", true)]
		public static void Cache() {}

		public static double Clamp(double value, double lowerBound = -1.0, double upperBound = 1.0)
		{
			if(lowerBound > upperBound)
				throw new ArgumentException("Lower bound is above upper bound.");
			value = Math.Max(lowerBound, value);
			value = Math.Min(upperBound, value);
			return value;
		}

		[Obsolete("Use a constant value...", true)]
		public static void Const() {}

		[Obsolete("Add to noise input parameters.", true)]
		public static void Displace() {}

		public static double Exponent(double value, double expon = 1.0)
		{
			return (Math.Pow(Math.Abs((value + 1.0) / 2.0), expon) * 2.0 - 1.0);
		}

		[Obsolete("Use -value instead.")]
		public static double Invert(double value)
		{
			return -value;
		}

		[Obsolete("Use Math.Max(value1, value2).")]
		public static double Max(double value1, double value2)
		{
			return Math.Max(value1, value2);
		}

		[Obsolete("Use Math.Min(value1, value2).")]
		public static double Min(double value1, double value2)
		{
			return Math.Min(value1, value2);
		}

		[Obsolete("Use '*' operator.")]
		public static double Multiply(double value1, double value2)
		{
			return value1 * value2;
		}

		[Obsolete("Use Math.Pow(valueBase, valueExponent).")]
		public static double Power(double valueBase, double valueExponent)
		{
			return Math.Pow(valueBase, valueExponent);
		}
		
		[Obsolete("Use a rotation matrix of some kind, sharpdx has one.", true)]
		public static void RotatePoint() {}

		[Obsolete("Use 'value * scale + bias'.")]
		public static double ScaleBias(double value, double scale, double bias)
		{
			return value * scale + bias;
		}

		[Obsolete("Multiply the noise input parameters.", true)]
		public static void ScalePoint() {}

		public static double Select(double value1, double value2, double control, double lowerBound = -1.0, double upperBound = 1.0, double edgeFalloff = 0.0)
		{
			if (lowerBound > upperBound)
				throw new ArgumentException("Lower bound is above upper bound.");

			if (edgeFalloff > 0.0)
			{
				if (control < (lowerBound - edgeFalloff))
				{
					// The output value from the control module is below the selector
					// threshold; return the output value from the first source module.
					return value1;
				}
				if (control < (lowerBound + edgeFalloff))
				{
					// The output value from the control module is near the lower end of the
					// selector threshold and within the smooth curve. Interpolate between
					// the output values from the first and second source modules.
					var lowerCurve = (lowerBound - edgeFalloff);
					var upperCurve = (lowerBound + edgeFalloff);
					var alpha = Interp.SCurve3((control - lowerCurve) / (upperCurve - lowerCurve));
					return Interp.LinearInterp(value1, value2, alpha);
				}
				if (control < (upperBound - edgeFalloff))
				{
					// The output value from the control module is within the selector
					// threshold; return the output value from the second source module.
					return value2;
				}
				if (control < (upperBound + edgeFalloff))
				{
					// The output value from the control module is near the upper end of the
					// selector threshold and within the smooth curve. Interpolate between
					// the output values from the first and second source modules.
					var lowerCurve = (upperBound - edgeFalloff);
					var upperCurve = (upperBound + edgeFalloff);
					var alpha = Interp.SCurve3((control - lowerCurve) / (upperCurve - lowerCurve));
					return Interp.LinearInterp(value2, value1, alpha);
				}
				// Output value from the control module is above the selector threshold;
				// return the output value from the first source module.
				return value1;
			}
			if (control < lowerBound || control > upperBound)
			{
				return value1;
			}
			return value2;
		}

		[Obsolete("Add to the input of the noise generator.", true)]
		public static void TranslatePoint() {}
	}
}
