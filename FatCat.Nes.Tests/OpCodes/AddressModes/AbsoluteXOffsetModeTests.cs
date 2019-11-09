using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AbsoluteXOffsetModeTests : AbsoluteModeTests
	{
		protected override string ExpectedName => "Absolute,X";

		public AbsoluteXOffsetModeTests()
		{
			addressMode = new AbsoluteXOffset(cpu);
		}
	}
}