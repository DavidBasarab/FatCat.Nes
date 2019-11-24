using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfNotEqual : BranchOpCode
	{
		public override string Name => "BNE";

		protected override CpuFlag Flag => CpuFlag.Zero;

		protected override bool FlagState => false;

		public BranchIfNotEqual(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}