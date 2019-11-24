using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Branching
{
	public class BranchIfOverflowClear : BranchOpCode
	{
		public override string Name => "BVC";

		protected override CpuFlag Flag => CpuFlag.Overflow;

		protected override bool FlagState => false;

		public BranchIfOverflowClear(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}