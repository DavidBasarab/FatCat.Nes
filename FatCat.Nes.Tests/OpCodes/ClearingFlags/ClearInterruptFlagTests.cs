using FatCat.Nes.OpCodes.ClearingFlags;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.ClearingFlags
{
	[UsedImplicitly]
	public class ClearInterruptFlagTests : ClearFlagTests
	{
		protected override string ExpectedName => "CLI";

		protected override CpuFlag Flag => CpuFlag.DisableInterrupts;

		public ClearInterruptFlagTests() => opCode = new ClearInterruptFlag(cpu, addressMode);
	}
}