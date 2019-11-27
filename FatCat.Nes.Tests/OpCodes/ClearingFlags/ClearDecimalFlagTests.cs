using FatCat.Nes.OpCodes.ClearingFlags;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.ClearingFlags
{
	[UsedImplicitly]
	public class ClearDecimalFlagTests : ClearFlagTests
	{
		protected override string ExpectedName => "CLD";

		protected override CpuFlag Flag => CpuFlag.DecimalMode;

		public ClearDecimalFlagTests() => opCode = new ClearDecimalFlag(cpu, addressMode);
	}
}