using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AccumulatorModeTests : AddressModeTests
	{
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Accumulator";

		public AccumulatorModeTests() => addressMode = new AccumulatorMode(cpu);
	}
}