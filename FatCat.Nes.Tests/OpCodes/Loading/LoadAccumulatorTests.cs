using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.Loading;
using FluentAssertions;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	public class LoadAccumulatorTests : LoadingTests
	{
		protected override string ExpectedName => "LDA";

		public LoadAccumulatorTests() => opCode = new LoadAccumulator(cpu, addressMode);

		protected override void VerifyCpuValueSet() => cpu.Accumulator.Should().Be(FetchedData);
	}
}