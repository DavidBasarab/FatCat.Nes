using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PushStatusRegisterToStack : OpCode
	{
		public override string Name => "PHP";

		public PushStatusRegisterToStack(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var status = cpu.StatusRegister | CpuFlag.Break | CpuFlag.Unused;
			
			PushToStack((byte)status);
			
			cpu.RemoveFlag(CpuFlag.Break);
			cpu.RemoveFlag(CpuFlag.Unused);
			
			return 0;
		}
	}
}