using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfCarry : BranchOpCode
	{
		public override string Name => "BCS";

		public BranchIfCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => PerformBranch(CpuFlag.CarryBit);
	}
}