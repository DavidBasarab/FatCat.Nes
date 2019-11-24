using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	public class BranchIfEqualTests : BranchTests
	{
		protected override string ExpectedName => "BEQ";

		protected override CpuFlag Flag => CpuFlag.Zero;

		public BranchIfEqualTests() => opCode = new BranchIfEqual(cpu, addressMode);
	}
}