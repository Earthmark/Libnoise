namespace Noise.Modules
{
	public class Invert : Module
	{
		public Invert(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public Module ConnectedModule { get; set; }

		public override double GetValue(double x, double y, double z)
		{
			return -(ConnectedModule.GetValue(x, y, z));
		}
	}
}
