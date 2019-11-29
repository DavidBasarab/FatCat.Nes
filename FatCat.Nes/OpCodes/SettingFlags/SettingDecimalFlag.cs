using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.SettingFlags
{
	public class SettingDecimalFlag : SettingFlag
	{
		public override string Name => "SED";

		protected override CpuFlag Flag => CpuFlag.DecimalMode;

		public SettingDecimalFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}