using System;

namespace Noise.Modules
{
	/// <summary>
	/// Noise module that outputs a checkerboard pattern.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This noise module outputs unit-sized blocks of alternating values.
	/// The values of these blocks alternate between -1.0 and +1.0.
	/// </para>
	/// <para>
	/// This noise module is not really useful by itself, but it is often used
	/// for debugging purposes.
	/// </para>
	/// <para>
	/// This noise module does not require any source modules.
	/// </para>
	/// </remarks>
	public class Checkerboard : Module
	{
		/// <summary>
		/// Generates an output value given the coordinates of the specified input value. 
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x, double y, double z]
		{
			get
			{
				var ix = (int)(Math.Floor(NoiseGen.MakeInt32Range(x)));
				var iy = (int)(Math.Floor(NoiseGen.MakeInt32Range(y)));
				var iz = (int)(Math.Floor(NoiseGen.MakeInt32Range(z)));
				return (ix & 1 ^ iy & 1 ^ iz & 1) != 0 ? -1.0 : 1.0;
			}
		}
	}
}