using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class Break : OpCode
	{
		public override string Name => "BRK";

		public Break(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.SetFlag(CpuFlag.DisableInterrupts);
			
			return -1;
		}
	}
}