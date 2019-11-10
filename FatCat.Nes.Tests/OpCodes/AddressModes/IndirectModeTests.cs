using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class IndirectModeTests : AddressModeTests
	{
		public IndirectModeTests()
		{
			addressMode = new IndirectMode(cpu);
		}
		
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Indirect";
	}
}