﻿using System;

namespace Noiselib.Modules
{
	public class Voronoi : Module
	{
		public const double DefaultVoronoiDisplacement = 1.0;
		public const double DefaultVoronoiFrequency = 1.0;
		public const int DefaultVoronoiSeed = 0;

		public Voronoi()
		{
			Frequency = DefaultVoronoiFrequency;
			Displacement = DefaultVoronoiDisplacement;
			EnableDistance = false;
			Seed = DefaultVoronoiSeed;
		}

		public Voronoi(double frequency, double displacement, int seed, bool enableDistance)
		{
			Frequency = frequency;
			Displacement = displacement;
			EnableDistance = enableDistance;
			Seed = seed;
		}

		public double Displacement { get; set; }
		public bool EnableDistance { get; set; }
		public double Frequency { get; set; }
		public int Seed { get; set; }

		public override double this[double x, double y, double z]
		{
			get
			{
				// This method could be more efficient by caching the seed values.  Fix
				// later.

				x *= Frequency;
				y *= Frequency;
				z *= Frequency;

				int xInt = (x > 0.0 ? (int)x : (int)x - 1);
				int yInt = (y > 0.0 ? (int)y : (int)y - 1);
				int zInt = (z > 0.0 ? (int)z : (int)z - 1);

				double minDist = 2147483647.0;
				double xCandidate = 0;
				double yCandidate = 0;
				double zCandidate = 0;

				// Inside each unit cube, there is a seed point at a random position.  Go
				// through each of the nearby cubes until we find a cube with a seed point
				// that is closest to the specified position.
				for(int zCur = zInt - 2; zCur <= zInt + 2; zCur++)
				{
					for(int yCur = yInt - 2; yCur <= yInt + 2; yCur++)
					{
						for(int xCur = xInt - 2; xCur <= xInt + 2; xCur++)
						{
							// Calculate the position and distance to the seed point inside of
							// this unit cube.
							double xPos = xCur + NoiseGen.ValueNoise3D(xCur, yCur, zCur, Seed);
							double yPos = yCur + NoiseGen.ValueNoise3D(xCur, yCur, zCur, Seed + 1);
							double zPos = zCur + NoiseGen.ValueNoise3D(xCur, yCur, zCur, Seed + 2);
							double xDist = xPos - x;
							double yDist = yPos - y;
							double zDist = zPos - z;
							double dist = xDist * xDist + yDist * yDist + zDist * zDist;

							if(dist < minDist)
							{
								// This seed point is closer to any others found so far, so record
								// this seed point.
								minDist = dist;
								xCandidate = xPos;
								yCandidate = yPos;
								zCandidate = zPos;
							}
						}
					}
				}

				double value;
				if(EnableDistance)
				{
					// Determine the distance to the nearest seed point.
					double xDist = xCandidate - x;
					double yDist = yCandidate - y;
					double zDist = zCandidate - z;
					value = (Math.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist)) * MathConsts.Sqrt3 - 1.0;
				}
				else
				{
					value = 0.0;
				}

				// Return the calculated distance with the displacement value applied.
				return value + (Displacement * NoiseGen.ValueNoise3D(
					(int)(Math.Floor(xCandidate)),
					(int)(Math.Floor(yCandidate)),
					(int)(Math.Floor(zCandidate))));
			}
		}
	}
}