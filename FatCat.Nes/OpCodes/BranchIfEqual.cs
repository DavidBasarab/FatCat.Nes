using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfEqual : BranchOpCode
	{
		public override string Name => "BEQ";

		public BranchIfEqual(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => PerformBranch(CpuFlag.Zero);
	}
}