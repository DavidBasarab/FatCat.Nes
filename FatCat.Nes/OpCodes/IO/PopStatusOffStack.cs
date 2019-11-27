using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PopStatusOffStack : OpCode
	{
		public override string Name => "PLP";

		public PopStatusOffStack(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.StatusRegister = (CpuFlag)ReadFromStack();
			
			cpu.RemoveFlag(CpuFlag.Unused);
			
			return 0;
		}
	}
}