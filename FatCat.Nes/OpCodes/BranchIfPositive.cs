using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfPositive : BranchOpCode
	{
		public override string Name => "BPL";

		protected override CpuFlag Flag => CpuFlag.Negative;

		protected override bool FlagState => false;

		public BranchIfPositive(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}