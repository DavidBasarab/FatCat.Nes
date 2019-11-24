using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfNegative : BranchOpCode
	{
		public override string Name => "BMI";

		public BranchIfNegative(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => PerformBranch(CpuFlag.Negative);
	}
}