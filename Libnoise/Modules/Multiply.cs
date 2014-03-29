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

		public override double GetValue(double x, double y, double z)
		{
			return SourceModule1.GetValue(x, y, z) * SourceModule2.GetValue(x, y, z);
		}
	}
}