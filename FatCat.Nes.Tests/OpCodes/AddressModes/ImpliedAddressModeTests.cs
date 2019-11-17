using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImpliedAddressModeTests : AddressModeTests
	{
		private const int AccumulatorValue = 0x52;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Implied";

		public ImpliedAddressModeTests()
		{
			addressMode = new ImpliedAddressMode(cpu);

			cpu.Accumulator = AccumulatorValue;
		}

		[Fact]
		public void WillSetFetchedValueFromTheAccumulator()
		{
			addressMode.Run();

			cpu.Fetched.Should().Be(AccumulatorValue);
		}

		protected override void VerifyFetchedValue(byte fetchedValue, byte startingFetchedValue) => cpu.Fetched.Should().Be(startingFetchedValue);

		protected override void VerifyFetchResult(byte fetchResult, byte fetchedValue, byte startingFetchedValue) => fetchResult.Should().Be(startingFetchedValue);

		protected override void VerifyReadFromAbsoluteAddress(ushort absoluteAddress) => A.CallTo(() => cpu.Read(absoluteAddress)).MustNotHaveHappened();
	}
}