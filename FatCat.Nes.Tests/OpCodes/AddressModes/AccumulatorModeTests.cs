using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AccumulatorModeTests : AddressModeTests
	{
		private const int AccumulatorValue = 0x15;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Accumulator";

		public AccumulatorModeTests()
		{
			cpu.Accumulator = AccumulatorValue;

			addressMode = new AccumulatorMode(cpu);
		}

		[Fact]
		public void WillSetFetchedValueFromTheAccumulator()
		{
			addressMode.Run();

			cpu.Fetched.Should().Be(AccumulatorValue);
		}
	}
}