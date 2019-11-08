using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class RelativeModeTests : AddressModeTests
	{
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Relative";

		public RelativeModeTests() => addressMode = new Relative(cpu);

		[Theory]
		[InlineData(131)]
		[InlineData(212)]
		[InlineData(254)]
		public void WillChangeRelativeAddressIsBitwiseAndIsGreaterThan0(byte readValue)
		{
			cpu.ProgramCounter = ProgramCounter;
			
			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(readValue);

			addressMode.Run();

			var expectedAddress = (ushort)(readValue | 0xff00);

			cpu.RelativeAddress.Should().Be(expectedAddress);
		}

		[Fact]
		public void WillIncrementTheProgramCounter()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(127)]
		public void WillNotChangeIfBitwiseCompareIs0(byte readValue)
		{
			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(readValue);

			addressMode.Run();

			var expectedAddress = readValue;

			cpu.RelativeAddress.Should().Be(expectedAddress);
		}

		[Fact]
		public void WillReadTheProgramCounterFromTheCpu()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}