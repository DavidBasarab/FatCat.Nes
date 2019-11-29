using FatCat.Nes.OpCodes.SettingFlags;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.SettingFlags
{
	[UsedImplicitly]
	public class SettingInterruptFlagTests : SettingFlagTests
	{
		protected override string ExpectedName => "SEI";

		protected override CpuFlag Flag => CpuFlag.DisableInterrupts;

		public SettingInterruptFlagTests() => opCode = new SettingInterruptFlag(cpu, addressMode);
	}
}