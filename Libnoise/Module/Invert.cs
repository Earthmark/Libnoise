namespace Noise.Module
{
	public class Invert : Module
	{
		public Module ConnectedModule { get; set; }

		public Invert(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public override double GetValue(double x, double y, double z)
		{
			return -(ConnectedModule.GetValue(x, y, z));
		}
	}
}
