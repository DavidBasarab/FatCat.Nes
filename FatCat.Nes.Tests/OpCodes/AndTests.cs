using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AndTests : OpCodeTest
	{
		public AndTests() => opCode = new And(cpu, addressMode);

		protected override string ExpectedName => "AND";
	}
}