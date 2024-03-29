using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImmediateModeTests : AddressModeTests
	{
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Immediate";

		public ImmediateModeTests()
		{
			addressMode = new ImmediateMode(cpu);

			cpu.ProgramCounter = ProgramCounter;
		}

		[Fact]
		public void WillIncreaseTheProgramCounterBy1()
		{
			addressMode.Run();

			ushort expectedProgramCounter = ProgramCounter + 1;

			cpu.ProgramCounter.Should().Be(expectedProgramCounter);
		}

		[Fact]
		public void WillSetTheAbsoluteAddressToProgramCounter()
		{
			addressMode.Run();

			cpu.AbsoluteAddress.Should().Be(ProgramCounter);
		}
	}
}