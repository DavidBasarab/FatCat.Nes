using System;
using JetBrains.Annotations;

namespace FatCat.OneOff.Common
{
	[PublicAPI]
	[Flags]
	public enum NewLineLocation
	{
		None = 0,
		Before = 1,
		After = 2,
		Both = Before | After
	}
}