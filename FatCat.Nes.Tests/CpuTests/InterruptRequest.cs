using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class InterruptRequest : CpuBaseTests
	{
		private const int ProgramCounter = 0xd1b2;
		private const int StartingStackPointer = 0xe1;

		public InterruptRequest()
		{
			cpu.StackPointer = StartingStackPointer;
			cpu.ProgramCounter = ProgramCounter;
		}

		[Fact]
		public void WillReduceTheStackPointerBy1WhenHighMemoryWritten()
		{
			cpu.Irq();

			cpu.StackPointer.Should().Be(0xe0);
		}

		[Fact]
		public void WillWriteHighMemoryToStack()
		{
			cpu.Irq();

			bus.Verify(v => v.Write(0x0100 + StartingStackPointer, 0xd1));
		}
	}
}