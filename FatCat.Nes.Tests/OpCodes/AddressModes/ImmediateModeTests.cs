using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImmediateModeTests : AddressModeTests
	{
		private const ushort ProgramCounter = 0x53E2;

		protected override string ExpectedName => "Immediate";

		public ImmediateModeTests()
		{
			addressMode = new ImmediateMode(cpu);

			cpu.ProgramCounter = ProgramCounter;
		}

		[Fact]
		public void RunWillTake0Cycles()
		{
			var cycles = addressMode.Run();

			cycles.Should().Be(0);
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