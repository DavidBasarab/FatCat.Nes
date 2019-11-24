using FatCat.Nes.OpCodes;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.ClearingFlags
{
	[UsedImplicitly]
	public class ClearCarryFlagTests : ClearFlagTests
	{
		protected override string ExpectedName => "CLC";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		public ClearCarryFlagTests() => opCode = new ClearCarryFlag(cpu, addressMode);
	}
}