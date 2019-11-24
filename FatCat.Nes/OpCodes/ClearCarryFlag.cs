using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ClearCarryFlag : OpCode
	{
		public override string Name => "CLC";

		public ClearCarryFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.RemoveFlag(CpuFlag.CarryBit);
			
			return 0;
		}
	}
}