using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class CompareAccumulatorTests : OpCodeTest
	{
		protected override string ExpectedName => "CMP";

		public CompareAccumulatorTests() => opCode = new CompareAccumulator(cpu, addressMode);
	}
}