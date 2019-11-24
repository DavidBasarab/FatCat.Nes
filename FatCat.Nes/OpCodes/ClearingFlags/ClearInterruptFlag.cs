using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.ClearingFlags
{
	public class ClearInterruptFlag : ClearFlagOpCode
	{
		public override string Name => "CLI";

		protected override CpuFlag Flag => CpuFlag.DisableInterrupts;

		public ClearInterruptFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}