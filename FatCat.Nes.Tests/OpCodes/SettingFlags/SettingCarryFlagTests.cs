using FatCat.Nes.OpCodes.SettingFlags;

namespace FatCat.Nes.Tests.OpCodes.SettingFlags
{
	public class SettingCarryFlagTests : SettingFlagTests
	{
		protected override string ExpectedName => "SEC";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		public SettingCarryFlagTests() => opCode = new SettingCarryFlag(cpu, addressMode);
	}
}