namespace Noiselib.Modules
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

		public override double this[double x, double y]
		{
			get { return -(ConnectedModule[x, y]); }
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x]
		{
			get { return -(ConnectedModule[x]); }
		}
	}
}