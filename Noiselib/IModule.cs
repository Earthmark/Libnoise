namespace Noiselib
{
	internal interface IModule
	{
		double this[double x] { get; }
		double this[double x, double y] { get; }
		double this[double x, double y, double z] { get; }
		double this[double x, double y, double z, double w] { get; }
	}
}
