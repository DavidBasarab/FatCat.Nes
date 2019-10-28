using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class InterruptRequest : CpuBaseTests
	{
		public InterruptRequest()
		{
			cpu.StackPointer = 0xe1;
			cpu.ProgramCounter = 0xd1b2;
		}
		
		[Fact]
		public void WillWriteHighMemoryToStack()
		{
			cpu.Irq();
			
			bus.Verify(v => v.Write((ushort)(0x0100 + cpu.StackPointer), 0xd1));
		}
	}
}