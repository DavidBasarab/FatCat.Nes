using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AbsoluteModeTests : AddressModeTests
	{
		private const byte HighAddress = 0xe4;

		private const byte LowAddress = 0x3d;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Absolute";

		public AbsoluteModeTests()
		{
			addressMode = new Absolute(cpu);

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(LowAddress);
			A.CallTo(() => cpu.Read(ProgramCounter + 1)).Returns(HighAddress);
		}

		[Fact]
		public void ProgramCounterWillIncrementBy2()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 2);
		}

		[Fact]
		public void WillReadFromProgramCounterAgainForHighMemory()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter + 1)).MustHaveHappened();
		}

		[Fact]
		public void WillReadFromStartingProgramCounterForLowAddress()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheAbsoluteAddressToBothHighAndLowAddress()
		{
			cpu.XRegister = 0x00;
			cpu.YRegister = 0x00;

			addressMode.Run();

			cpu.AbsoluteAddress.Should().Be(0xe43d);
		}
	}
}