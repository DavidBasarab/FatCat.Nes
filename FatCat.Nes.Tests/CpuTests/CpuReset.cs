using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuReset : CpuBaseTests
	{
		public CpuReset()
		{
			cpu.Accumulator = 0x78;
			cpu.XRegister = 0x12;
			cpu.YRegister = 0x56;
			cpu.StackPointer = 0xF3;
			cpu.StatusRegister = CpuFlag.Break | CpuFlag.Overflow | CpuFlag.CarryBit;
			cpu.ProgramCounter = 0x7834;
			cpu.Cycles = 3482;
			cpu.ClockCount = 9003;
		}
		
		[Fact]
		public void WillReadLowAddress()
		{
			cpu.Reset();
			
			bus.Verify(v => v.Read(0xfffc));
		}
		
		[Fact]
		public void WillReadHighAddress()
		{
			cpu.Reset();
			
			bus.Verify(v => v.Read(0xfffd));
		}
		
		[Fact]
		public void WillSetProgramCounterToAddressRead()
		{
			byte lowAddress = 0x11;
			byte highAddress = 0x22;
			
			bus.Setup(v => v.Read(0xfffc)).Returns(lowAddress);
			bus.Setup(v => v.Read(0xfffd)).Returns(highAddress);
			
			cpu.Reset();

			cpu.ProgramCounter.Should().Be(0x2211);
		}
	}
}