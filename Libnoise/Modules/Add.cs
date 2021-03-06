﻿using System;

namespace Noise.Modules
{
	/// <summary>
	///      A noise module that retrieves the sum of the two output values from two other source modules.
	/// </summary>
	public class Add : Module
	{
		/// <summary>
		///      Creates a new add module, without setting the NoiseModule1 or NoiseModule2.
		/// </summary>
		public Add() {}

		/// <summary>
		///      Creates a new add module, also setting the NoiseModule1 or NoiseModule2.
		/// </summary>
		/// <param name="noiseModule1">The first module to connect to this add module is connected to.</param>
		/// <param name="noiseModule2">The second module this add module is connected to.</param>
		public Add(Module noiseModule1, Module noiseModule2)
		{
			NoiseModule1 = noiseModule1;
			NoiseModule2 = noiseModule2;
		}

		/// <summary>
		///      The first module this add module is connected to.
		/// </summary>
		public Module NoiseModule1 { get; set; }

		/// <summary>
		///      The second module this add module is connected to.
		/// </summary>
		public Module NoiseModule2 { get; set; }

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <exception cref="NullReferenceException">Thrown if NoiseModule1 or NoiseModule2 are null.</exception>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double GetValue(double x, double y, double z)
		{
			if(NoiseModule1 == null || NoiseModule2 == null)
			{
				throw new NullReferenceException("NoiseModule1 or NoiseModule2 are null.");
			}
			return NoiseModule1.GetValue(x, y, z) + NoiseModule2.GetValue(x, y, z);
		}
	}
}