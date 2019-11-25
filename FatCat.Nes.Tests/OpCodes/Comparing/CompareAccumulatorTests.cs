using FatCat.Nes.OpCodes.Comparing;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Comparing
{
	[UsedImplicitly]
	public class CompareAccumulatorTests : CompareRegisterTests
	{
		protected override string ExpectedName => "CMP";

		public CompareAccumulatorTests() => opCode = new CompareAccumulator(cpu, addressMode);

		protected override void SetRegister(byte registerValue) => cpu.Accumulator = registerValue;
	}
}