using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.SettingFlags
{
	public class SettingCarryFlag : SettingFlag
	{
		public override string Name => "SEC";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		public SettingCarryFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}