using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BitOpCodeTests : OpCodeTest
	{
		protected override string ExpectedName => "BIT";

		public BitOpCodeTests()
		{
			opCode = new BitOpCode(cpu, addressMode);
		}
	}
}