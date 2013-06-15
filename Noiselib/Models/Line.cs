using Noiselib.Modules;

namespace Noiselib.Models
{
	/// <summary>
	/// Model that defines the displacement of a line segment.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This model returns an output value from a noise method given the
	/// one-dimensional coordinate of an input value located on a line
	/// segment, which can be used as displacements.
	/// </para>
	/// <para>
	/// This class is useful for creating roads and rivers.
	/// </para>
	/// <para>
	/// To generate an output value, pass an input value between 0.0 and 1.0
	/// to the GetValue method. 0.0 represents the start position of the
	/// line segment and 1.0 represents the end position of the line segment.
	/// </para>
	/// </remarks>
	public class Line
	{
		/// <summary>
		/// Constructor, does not bind or set any values.
		/// </summary>
		public Line() {}

		/// <summary>
		/// Constructor, binds a method to the line.
		/// </summary>
		/// <param name="sourceMethod">The method to encapsulate.</param>
		public Line(Module sourceMethod)
		{
			SourceMethod = sourceMethod;
		}

		/// <summary>
		/// Constructor, Sets the start and end points.
		/// </summary>
		/// <param name="startX">The x coordinate of the start position.</param>
		/// <param name="startY">The y coordinate of the start position.</param>
		/// <param name="startZ">The z coordinate of the start position.</param>
		/// <param name="endX">The x coordinate of the end position.</param>
		/// <param name="endY">The y coordinate of the end position.</param>
		/// <param name="endZ">The z coordinate of the end position.</param>
		public Line(double startX, double startY, double startZ, double endX, double endY, double endZ)
		{
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		/// <summary>
		/// Constructor, binds a method, as well as sets the start and end points.
		/// </summary>
		/// <param name="sourceMethod">The method to encapsulate.</param>
		/// <param name="startX">The x coordinate of the start position.</param>
		/// <param name="startY">The y coordinate of the start position.</param>
		/// <param name="startZ">The z coordinate of the start position.</param>
		/// <param name="endX">The x coordinate of the end position.</param>
		/// <param name="endY">The y coordinate of the end position.</param>
		/// <param name="endZ">The z coordinate of the end position.</param>
		public Line(Module sourceMethod, double startX, double startY, double startZ, double endX, double endY, double endZ)
		{
			SourceMethod = sourceMethod;
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		/// <summary>
		/// Constructor, binds a method as well as sets the attenuate flag.
		/// </summary>
		/// <param name="sourceMethod">The method to encapsulate.</param>
		/// <param name="attenuate">Returns a flag indicating whether the output value is to be attenuated</param>
		public Line(Module sourceMethod, bool attenuate)
		{
			Attenuate = attenuate;
			SourceMethod = sourceMethod;
		}

		/// <summary>
		/// Constructor, sets the start and end points as well as setting the attenuate flag.
		/// </summary>
		/// <param name="startX">The x coordinate of the start position.</param>
		/// <param name="startY">The y coordinate of the start position.</param>
		/// <param name="startZ">The z coordinate of the start position.</param>
		/// <param name="endX">The x coordinate of the end position.</param>
		/// <param name="endY">The y coordinate of the end position.</param>
		/// <param name="endZ">The z coordinate of the end position.</param>
		/// <param name="attenuate">Returns a flag indicating whether the output value is to be attenuated</param>
		public Line(double startX, double startY, double startZ, double endX, double endY, double endZ, bool attenuate)
		{
			Attenuate = attenuate;
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		/// <summary>
		/// Constructor, binds a method as well as setting the start and end points, while also setting the attenuate flag.
		/// </summary>
		/// <param name="sourceMethod">The method to encapsulate.</param>
		/// <param name="startX">The x coordinate of the start position.</param>
		/// <param name="startY">The y coordinate of the start position.</param>
		/// <param name="startZ">The z coordinate of the start position.</param>
		/// <param name="endX">The x coordinate of the end position.</param>
		/// <param name="endY">The y coordinate of the end position.</param>
		/// <param name="endZ">The z coordinate of the end position.</param>
		/// <param name="attenuate">Returns a flag indicating whether the output value is to be attenuated</param>
		public Line(Module sourceMethod, double startX, double startY, double startZ, double endX, double endY, double endZ, bool attenuate)
		{
			Attenuate = attenuate;
			SourceMethod = sourceMethod;
			StartX = startX;
			StartY = startY;
			StartZ = startZ;
			EndX = endX;
			EndY = endY;
			EndZ = endZ;
		}

		/// <summary>
		/// A flag that specifies whether the value is to be attenuated
		/// (moved toward 0.0) as the ends of the line segment are approached.
		/// </summary>
		public bool Attenuate { get; set; }
		/// <summary>
		/// The noise module that is encapsulated.
		/// </summary>
		public Module SourceMethod { get; set; }

		/// <summary>
		/// x coordinate of the start of the line segment.
		/// </summary>
		public double StartX { get; set; }
		/// <summary>
		/// y coordinate of the start of the line segment.
		/// </summary>
		public double StartY { get; set; }
		/// <summary>
		/// z coordinate of the start of the line segment.
		/// </summary>
		public double StartZ { get; set; }

		/// <summary>
		/// x coordinate of the end of the line segment.
		/// </summary>
		public double EndX { get; set; }
		/// <summary>
		/// y coordinate of the end of the line segment.
		/// </summary>
		public double EndY { get; set; }
		/// <summary>
		/// z coordinate of the end of the line segment.
		/// </summary>
		public double EndZ { get; set; }

		/// <summary>
		/// Sets the position ( x, y, z ) of the start of the line
		/// segment to choose values along.
		/// </summary>
		/// <param name="x">x coordinate of the start position.</param>
		/// <param name="y">y coordinate of the start position.</param>
		/// <param name="z">z coordinate of the start position.</param>
		public void SetStartPoint(double x, double y, double z)
		{
			StartX = x;
			StartY = y;
			StartZ = z;
		}

		/// <summary>
		/// Sets the position ( x, y, z ) of the end of the line
		/// segment to choose values along.
		/// </summary>
		/// <param name="x">x coordinate of the end position.</param>
		/// <param name="y">y coordinate of the end position.</param>
		/// <param name="z">z coordinate of the end position.</param>
		public void SetEndPoint(double x, double y, double z)
		{
			EndX = x;
			EndY = y;
			EndZ = z;
		}

		/// <summary>
		/// Returns the output value from the noise method given the
		/// one-dimensional coordinate of the specified input value located
		/// on the line segment.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The output may be attenuated (moved toward 0.0) as p
		/// approaches either end of the line segment; this is
		/// the default behavior.
		/// </para>
		/// <para>
		/// If the value is not to be attenuated, p can safely range
		/// outside the 0.0 to 1.0 range; the output value will be
		/// extrapolated along the line that this segment is part of.
		/// </para>
		/// </remarks>
		/// <param name="p">The distance along the line segment (ranges from 0.0 to 1.0)</param>
		/// <returns>The output value from the noise method.</returns>
		public double GetValue(double p)
		{
			var x = (EndX - StartX) * p + StartX;
			var y = (EndY - StartY) * p + StartY;
			var z = (EndZ - StartZ) * p + StartZ;
			var value = SourceMethod(x, y, z);

			return Attenuate ? p * (1.0 - p) * 4 * value : value;
		}
	}
}
