namespace Noiselib.Generators
{
	public sealed class Checkerboard
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
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public double GetValue(double x, double y, double z)
		{
			//Relies on truncation.
			var ix = (int)NoiseGen.MakeInt32Range(x);
			var iy = (int)NoiseGen.MakeInt32Range(y);
			var iz = (int)NoiseGen.MakeInt32Range(z);
			return (ix & 1 ^ iy & 1 ^ iz & 1) != 0 ? -1.0 : 1.0;
		}
	}
}
