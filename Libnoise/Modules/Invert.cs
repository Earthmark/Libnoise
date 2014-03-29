namespace Noise.Modules
{
	public class Invert : Module
	{
		public Invert(Module connectedModule)
		{
			ConnectedModule = connectedModule;
		}

		public Module ConnectedModule { get; set; }

		public override double this[double x, double y, double z]
		{
			get { return -(ConnectedModule[x, y, z]); }
		}
	}
}