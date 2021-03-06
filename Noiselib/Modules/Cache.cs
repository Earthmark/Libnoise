﻿using System;

namespace Noiselib.Modules
{
	/// <summary>
	///      Noise module that caches the last output value generated by a source
	///      module.
	/// </summary>
	/// <remarks>
	///      <para>
	///           If an application passes an input value to the GetValue() method that
	///           differs from the previously passed-in input value, this noise module
	///           instructs the source module to calculate the output value.  This
	///           value, as well as the ( @a x, @a y, @a z ) coordinates of the input
	///           value, are stored (cached) in this noise module.
	///      </para>
	///      <para>
	///           If the application passes an input value to the GetValue() method
	///           that is equal to the previously passed-in input value, this noise
	///           module returns the cached output value without having the source
	///           module recalculate the output value.
	///      </para>
	///      <para>
	///           If an application passes a new source module to the SetSourceModule()
	///           method, the cache is invalidated.
	///      </para>
	///      <para>
	///           Caching a noise module is useful if it is used as a source module for
	///           multiple noise modules.  If a source module is not cached, the source
	///           module will redundantly calculate the same output value once for each
	///           noise module in which it is included.
	///      </para>
	///      <para>
	///           This noise module requires one source module.
	///      </para>
	/// </remarks>
	public class Cache : Module
	{
		private const double Epsilon = 0.0000001;

		private Module sourceModule;

		/// <summary>
		///      Constructor, does not bind a source module.
		/// </summary>
		public Cache() {}

		/// <summary>
		///      Constructor, does bind a source module.
		/// </summary>
		/// <param name="sourceModule">The module to bind.</param>
		public Cache(Module sourceModule)
		{
			SourceModule = sourceModule;
		}

		/// <summary>
		///      Determines if a cached output value is stored in this noise
		///      module.
		/// </summary>
		public bool IsCached { get; private set; }

		/// <summary>
		///      The cached output value at the cached input value.
		/// </summary>
		public double CachedValue { get; private set; }

		/// <summary>
		///      The x coordinate of the cached input value.
		/// </summary>
		public double XCache { get; private set; }

		/// <summary>
		///      The y coordinate of the cached input value.
		/// </summary>
		public double YCache { get; private set; }

		/// <summary>
		///      The z coordinate of the cached input value.
		/// </summary>
		public double ZCache { get; private set; }

		/// <summary>
		///      The encapsulated module.
		/// </summary>
		public Module SourceModule
		{
			get { return sourceModule; }
			set
			{
				sourceModule = value;
				IsCached = false;
			}
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <param name="y">The y coordinate of the input value.</param>
		/// <param name="z">The z coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x, double y, double z]
		{
			get
			{
				if(!(IsCached && Math.Abs(x - XCache) < Epsilon && Math.Abs(y - YCache) < Epsilon && Math.Abs(z - ZCache) < Epsilon))
				{
					CachedValue = SourceModule[x, y, z];
					XCache = x;
					YCache = y;
					ZCache = z;
				}
				IsCached = true;
				return CachedValue;
			}
		}

		public override double this[double x, double y]
		{
			get
			{
				throw new NotImplementedException();
				if(!(IsCached && Math.Abs(x - XCache) < Epsilon && Math.Abs(y - YCache) < Epsilon))
				{
					CachedValue = SourceModule[x, y];
					XCache = x;
					YCache = y;
				}
				IsCached = true;
				return CachedValue;
			}
		}

		/// <summary>
		///      Generates an output value given the coordinates of the specified input value.
		/// </summary>
		/// <param name="x">The x coordinate of the input value.</param>
		/// <returns>The output value.</returns>
		public override double this[double x]
		{
			get
			{
				throw new NotImplementedException();
				if(!(IsCached && Math.Abs(x - XCache) < Epsilon))
				{
					CachedValue = SourceModule[x];
					XCache = x;
				}
				IsCached = true;
				return CachedValue;
			}
		}
	}
}