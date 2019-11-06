using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImmediateModeTests : AddressModeTests
	{
		protected override string ExpectedName => "Immediate";

		public ImmediateModeTests() => addressMode = new ImmediateMode(cpu);
	}
}