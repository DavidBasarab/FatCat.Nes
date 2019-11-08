using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class ProgramCounterRolloverTests : CpuBaseTests
	{
		[Fact]
		public void ProgramCounterWillGoTo0OnOverflow()
		{
			cpu.ProgramCounter = 0xFFFF;

			cpu.ProgramCounter++;

			cpu.ProgramCounter.Should().Be(0x0000);
		}
	}
}