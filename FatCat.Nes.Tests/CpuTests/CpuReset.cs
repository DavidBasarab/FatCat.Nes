namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuReset : CpuBaseTests
	{
		public CpuReset()
		{
			cpu.AbsoluteAddress = 0x786f;
			cpu.RelativeAddress = 0x3807;
			
		}
	}
}