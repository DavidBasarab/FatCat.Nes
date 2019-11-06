using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImpliedAddressModeTests
	{
		private const int AccumulatorValue = 0x52;
		private readonly ImpliedAddressMode addressMode;
		private readonly ICpu cpu;

		public ImpliedAddressModeTests()
		{
			cpu = A.Fake<ICpu>();

			addressMode = new ImpliedAddressMode(cpu);

			cpu.Accumulator = AccumulatorValue;
		}

		[Fact]
		public void WillHaveNameOfImplied() => addressMode.Name.Should().Be("Implied");

		[Fact]
		public void WillReturn0Cycles() => addressMode.Run().Should().Be(0);

		[Fact]
		public void WillSetFetchedValueFromTheAccumulator()
		{
			addressMode.Run();

			cpu.Fetched.Should().Be(AccumulatorValue);
		}
	}
}