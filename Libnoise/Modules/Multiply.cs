namespace Noise.Modules
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
	}
}
