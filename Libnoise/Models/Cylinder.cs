using System;
using Noise.Modules;

namespace Noise.Models
{
	/// <summary>
	/// Model that defines the surface of a cylinder.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This model returns an output value from a noise module given the
	/// coordinates of an input value located on the surface of a cylinder.
	/// </para>
	/// <para>
	/// To generate an output value, pass the (angle, height) coordinates of
	/// an input value to the GetValue() method.
	/// </para>
	/// <para>
	/// This model is useful for creating seamless textures that can be mapped onto a cylinder
	/// </para>
	/// <para>
	/// This cylinder has a radius of 1.0 unit and has infinite height.  It is
	/// oriented along the y axis.  Its center is located at the origin.
	/// </para>
	/// </remarks>
	public class Cylinder
	{
		/// <summary>
		/// Basic constructor, does not connect module.
		/// </summary>
		public Cylinder() {}

		/// <summary>
		/// Basic constructor, does connect to a module.
		/// </summary>
		/// <param name="sourceModule">The module to encapsulate.</param>
		public Cylinder(Module sourceModule)
		{
			SourceModule = sourceModule;
		}

		/// <summary>
		/// The encapsulated module.
		/// </summary>
		public Module SourceModule { get; set; }

		/// <summary>
		/// Returns the output value from the noise module given the
		/// (angle, height) coordinates of the specified input value located
		/// on the surface of the cylinder.
		/// </summary>
		/// <remarks>
		/// This cylinder has a radius of 1.0 unit and has infinite height.
		/// It is oriented along the y axis.  Its center is located at the
		/// origin.
		/// </remarks>
		/// <param name="angle">The angle around the cylinder's center, in degrees.</param>
		/// <param name="height">The height along the y axis.</param>
		/// <returns>The output value from the noise module.</returns>
		public double GetValue(double angle, double height)
		{
			var x = Math.Cos(angle * MathConsts.DegToRad);
			var y = height;
			var z = Math.Sin(angle * MathConsts.DegToRad);
			return SourceModule.GetValue(x, y, z);
		}
	}
}
