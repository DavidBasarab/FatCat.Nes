using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class LoadAccumulatorTests : OpCodeTest
	{
		protected override string ExpectedName => "LDA";

		public LoadAccumulatorTests()
		{
			opCode = new LoadAccumulator(cpu, addressMode);
		}
	}
}