using FatCat.Nes.OpCodes.ClearingFlags;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.ClearingFlags
{
	[UsedImplicitly]
	public class ClearOverflowFlagTests : ClearFlagTests
	{
		protected override string ExpectedName => "CLV";

		protected override CpuFlag Flag => CpuFlag.Overflow;

		public ClearOverflowFlagTests() => opCode = new ClearOverflowFlag(cpu, addressMode);
	}
}