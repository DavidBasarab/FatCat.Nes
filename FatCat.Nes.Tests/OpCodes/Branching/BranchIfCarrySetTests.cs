using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	public class BranchIfCarrySetTests : BranchTests
	{
		protected override string ExpectedName => "BCS";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		public BranchIfCarrySetTests() => opCode = new BranchIfCarry(cpu, addressMode);
	}
}