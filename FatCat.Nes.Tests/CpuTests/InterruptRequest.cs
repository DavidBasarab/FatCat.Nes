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
			cpu.StatusRegister = CpuFlag.Negative | CpuFlag.Zero | CpuFlag.Break;

			cpu.Irq();
		}

		[Fact]
		public void WillReduceTheStackPointer() => cpu.StackPointer.Should().Be(0xde);

		[Fact]
		public void WillWriteHighMemoryToStack() => bus.Verify(v => v.Write(0x0100 + StartingStackPointer, 0xd1));

		[Fact]
		public void WillWriteLowMemoryToStack() => bus.Verify(v => v.Write(0x0100 + (StartingStackPointer - 1), 0xb2));
		
		[Fact]
		public void WillPushTheStatusRegisterOnTheStack()
		{
			var expectedStatusRegister = CpuFlag.Negative | CpuFlag.Zero | CpuFlag.Unused | CpuFlag.DisableInterrupts;
			
			bus.Verify(v => v.Write(0x0100 + (StartingStackPointer - 2), (byte)expectedStatusRegister));
		}
	}
}