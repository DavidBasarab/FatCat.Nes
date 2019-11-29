using FatCat.Nes.OpCodes.SettingFlags;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.SettingFlags
{
	[UsedImplicitly]
	public class SettingDecimalFlagTests : SettingFlagTests
	{
		protected override string ExpectedName => "SED";

		protected override CpuFlag Flag => CpuFlag.DecimalMode;

		public SettingDecimalFlagTests() => opCode = new SettingDecimalFlag(cpu, addressMode);
	}
}