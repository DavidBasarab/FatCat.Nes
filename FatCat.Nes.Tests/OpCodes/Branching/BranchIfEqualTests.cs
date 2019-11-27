using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfEqualTests : BranchTests
	{
		protected override string ExpectedName => "BEQ";

		protected override CpuFlag Flag => CpuFlag.Zero;

		protected override bool FlagState => true;

		public BranchIfEqualTests() => opCode = new BranchIfEqual(cpu, addressMode);
	}
}