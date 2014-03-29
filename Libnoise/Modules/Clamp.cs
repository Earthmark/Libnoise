using System;

namespace Noise.Modules
{
	/// <summary>
	/// Noise module that clamps the output value from a source module to a
	/// range of values.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The range of values in which to clamp the output value is called the
	/// clamping range.
	/// </para>
	/// <para>
	/// If the output value from the source module is less than the lower
	/// bound of the clamping range, this noise module clamps that value to
	/// the lower bound.  If the output value from the source module is
	/// greater than the upper bound of the clamping range, this noise module
	/// clamps that value to the upper bound.
	/// </para>
	/// <para>
	/// To specify the upper and lower bounds of the clamping range, call the
	/// SetBounds method.
	/// </para>
	/// <para>
	/// This noise module requires one source module.
	/// </para>
	/// </remarks>
	public class Clamp : Module
	{
		/// <summary>
		/// Default lower bound of the clamping range for the noise.modules.Clamp
		/// noise module.
		/// </summary>
		public const double DefaultClampLowerBound = -1.0;

		/// <summary>
		/// Default upper bound of the clamping range for the noise.modules.Clamp
		/// noise module.
		/// </summary>
		public const double DefaultClampUpperBound = 1.0;

		public Clamp()
		{
			LowerBound = DefaultClampLowerBound;
			UpperBound = DefaultClampUpperBound;
		}

		public Clamp(Module sourceModule)
		{
			SourceModule = sourceModule;
			LowerBound = DefaultClampLowerBound;
			UpperBound = DefaultClampUpperBound;
		}

		public Clamp(double lowerBound, double upperBound)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;
		}

		public Clamp(Module sourceModule, double lowerBound, double upperBound)
		{
			SourceModule = sourceModule;
			LowerBound = lowerBound;
			UpperBound = upperBound;
		}

		/// <summary>
		/// The encapsilated module.
		/// </summary>
		public Module SourceModule { get; set; }

		/// <summary>
		/// Lower bound of the clamping range.
		/// </summary>
		public double LowerBound { get; set; }

		/// <summary>
		/// Upper bound of the clamping range.
		/// </summary>
		public double UpperBound { get; set; }

		/// <summary>
		/// Sets the lower and upper bounds of the clamping range.
		/// </summary>
		/// <remarks>
		/// If the output value from the source module is less than the lower
		/// bound of the clamping range, this noise module clamps that value
		/// to the lower bound.  If the output value from the source module
		/// is greater than the upper bound of the clamping range, this noise
		/// module clamps that value to the upper bound.
		/// </remarks>
		/// <exception cref="ArgumentException">Thrown if the lower bound is greater than the upper bound.</exception>
		/// <param name="lowerBound">The lower bound.</param>
		/// <param name="upperBound">The upper bound.</param>
		public void SetBounds(double lowerBound, double upperBound)
		{
			if(lowerBound > upperBound)
				throw new ArgumentException("Lower and upper bound overlap.");
			LowerBound = lowerBound;
			UpperBound = upperBound;
		}

		public override double this[double x, double y, double z]
		{
			get
			{
				var value = SourceModule[x, y, z];
				return value < LowerBound ? LowerBound : (value > UpperBound ? UpperBound : value);
			}
		}
	}
}
