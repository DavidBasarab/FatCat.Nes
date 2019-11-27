using FatCat.Nes.OpCodes.Branching;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	public class JumpTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x15;

		protected override string ExpectedName => "JMP";

		public JumpTests()
		{
			opCode = new Jump(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void WillSetTheProgramCounterToAbsoluteAddress()
		{
			opCode.Execute();

			cpu.ProgramCounter.Should().Be(AbsoluteAddress);
		}

		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}