using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfOverFlowSetTests : BranchTests
	{
		protected override string ExpectedName => "BVS";

		protected override CpuFlag Flag => CpuFlag.Overflow;

		protected override bool FlagState => true;

		public BranchIfOverFlowSetTests() => opCode = new BranchIfOverFlowSet(cpu, addressMode);
	}
}