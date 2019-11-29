using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.SettingFlags
{
	public class SettingCarryFlag : OpCode
	{
		public SettingCarryFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "SEC";

		public override int Execute()
		{
			cpu.SetFlag(CpuFlag.CarryBit);
			
			return 0;
		}
	}
}