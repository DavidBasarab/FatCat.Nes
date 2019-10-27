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
		}
	}
}