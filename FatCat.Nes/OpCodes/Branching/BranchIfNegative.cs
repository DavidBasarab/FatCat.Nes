using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Branching
{
	public class BranchIfNegative : BranchOpCode
	{
		public override string Name => "BMI";

		protected override CpuFlag Flag => CpuFlag.Negative;

		protected override bool FlagState => true;

		public BranchIfNegative(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}