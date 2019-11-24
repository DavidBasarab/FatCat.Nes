using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Branching
{
	public class BranchIfOverFlowSet : BranchOpCode
	{
		public override string Name => "BVS";

		protected override CpuFlag Flag => CpuFlag.Overflow;

		protected override bool FlagState => true;

		public BranchIfOverFlowSet(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}