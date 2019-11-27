using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfCarrySetTests : BranchTests
	{
		protected override string ExpectedName => "BCS";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		protected override bool FlagState => true;

		public BranchIfCarrySetTests() => opCode = new BranchIfCarry(cpu, addressMode);
	}
}