using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfNegative : BranchOpCode
	{
		public override string Name => "BMI";

		protected override CpuFlag Flag => CpuFlag.Negative;

		public BranchIfNegative(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}