namespace Noiselib.Models
{
	/// <summary>
	/// Model that defines the displacement of a line segment.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This model returns an output value from a noise module given the
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
		/// Returns the output value from the noise module given the
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
		/// <param name="value">The input value from the noise generator.</param>
		/// <param name="p">The distance along the line segment (ranges from 0.0 to 1.0)</param>
		/// <param name="x">The output x value.</param>
		/// <param name="y">The output y value.</param>
		/// <param name="z">The output z value.</param>
		public void GetValue(double value, double p, out double x, out double y, out double z)
		{
			x = (EndX - StartX) * p + StartX;
			y = (EndY - StartY) * p + StartY;
			z = (EndZ - StartZ) * p + StartZ;
		}

		public static double AttenuatePass(double value, double p)
		{
			return p * (1.0 - p) * 4 * value;
		}
	}
}
