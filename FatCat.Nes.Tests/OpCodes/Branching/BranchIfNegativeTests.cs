using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.Branching;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	[UsedImplicitly]
	public class BranchIfNegativeTests : BranchTests
	{
		protected override string ExpectedName => "BMI";

		protected override CpuFlag Flag => CpuFlag.Negative;

		protected override bool FlagState => true;

		public BranchIfNegativeTests() => opCode = new BranchIfNegative(cpu, addressMode);
	}
}