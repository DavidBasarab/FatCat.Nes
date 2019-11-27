using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfNotEqualTests : BranchTests
	{
		protected override string ExpectedName => "BNE";

		protected override CpuFlag Flag => CpuFlag.Zero;

		protected override bool FlagState => false;

		public BranchIfNotEqualTests() => opCode = new BranchIfNotEqual(cpu, addressMode);
	}
}