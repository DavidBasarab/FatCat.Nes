using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.SettingFlags
{
	public class SettingInterruptFlag : SettingFlag
	{
		public override string Name => "SEI";

		protected override CpuFlag Flag => CpuFlag.DisableInterrupts;

		public SettingInterruptFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}