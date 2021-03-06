﻿namespace Noiselib.Modules
{
	/// <summary>
	///      mod * scale + bias
	/// </summary>
	public class ScaleBias : Module
	{
		public const double DefaultBias = 0.0;
		public const double DefaultScale = 1.0;

		public ScaleBias()
		{
			Bias = DefaultBias;
			Scale = DefaultScale;
		}

		public ScaleBias(Module connectedModule)
		{
			ConnectedModule = connectedModule;
			Bias = DefaultBias;
			Scale = DefaultScale;
		}

		public ScaleBias(Module connectedModule, double scale, double bias)
		{
			ConnectedModule = connectedModule;
			Scale = scale;
			Bias = bias;
		}

		public double Bias { get; set; }
		public double Scale { get; set; }
		public Module ConnectedModule { get; set; }

		public override double this[double x, double y, double z]
		{
			get { return ConnectedModule[x, y, z] * Scale + Bias; }
		}

		public override double this[double x, double y]
		{
			get { return ConnectedModule[x, y] * Scale + Bias; }
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x]
		{
			get { return ConnectedModule[x] * Scale + Bias; }
		}
	}
}