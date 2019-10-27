using System;

namespace FatCat.Nes
{
	[Flags]
	internal enum Flags
	{
		None = 0,
		CarryBit = 1,
		Zero = 2,
		DisableInterrupts = 4,
		DecimalMode = 8,
		Break = 16,
		Unused = 32,
		Overflow = 64,
		Negative = 126
	}
}