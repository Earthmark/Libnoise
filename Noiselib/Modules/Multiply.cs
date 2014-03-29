namespace Noiselib.Modules
{
	public class Multiply : Module
	{
		public Multiply() {}

		public Multiply(Module sourceModule1, Module sourceModule2)
		{
			SourceModule2 = sourceModule2;
			SourceModule1 = sourceModule1;
		}

		public Module SourceModule1 { get; set; }
		public Module SourceModule2 { get; set; }

		public override double this[double x, double y, double z]
		{
			get { return SourceModule1[x, y, z] * SourceModule2[x, y, z]; }
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x, double y]
		{
			get { return SourceModule1[x, y] * SourceModule2[x, y]; }
		}

		public override double this[double x]
		{
			get { return SourceModule1[x] * SourceModule2[x]; }
		}
	}
}