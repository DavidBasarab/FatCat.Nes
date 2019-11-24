using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfPositiveTests : BranchTests
	{
		protected override string ExpectedName => "BPL";

		protected override CpuFlag Flag => CpuFlag.Negative;

		protected override bool FlagState => false;

		public BranchIfPositiveTests() => opCode = new BranchIfPositive(cpu, addressMode);
	}
}