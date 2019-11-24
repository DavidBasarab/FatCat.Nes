using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Branching
{
	public class BranchIfEqual : BranchOpCode
	{
		public override string Name => "BEQ";

		protected override CpuFlag Flag => CpuFlag.Zero;

		protected override bool FlagState => true;

		public BranchIfEqual(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}