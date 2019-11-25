using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.ClearingFlags
{
	public class ClearOverflowFlag : ClearFlagOpCode
	{
		public override string Name => "CLV";

		protected override CpuFlag Flag => CpuFlag.Overflow;

		public ClearOverflowFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}