using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BreakTests : OpCodeTest
	{
		protected override string ExpectedName => "BRK";

		public BreakTests() => opCode = new Break(cpu, addressMode);
	}
}