using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfEqual : BranchOpCode
	{
		public override string Name => "BEQ";

		protected override CpuFlag Flag => CpuFlag.Zero;

		public BranchIfEqual(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}