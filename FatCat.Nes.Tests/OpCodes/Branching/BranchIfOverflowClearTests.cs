using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfOverflowClearTests : BranchTests
	{
		protected override string ExpectedName => "BVC";

		protected override CpuFlag Flag => CpuFlag.Overflow;

		protected override bool FlagState => false;

		public BranchIfOverflowClearTests() => opCode = new BranchIfOverflowClear(cpu, addressMode);
	}
}