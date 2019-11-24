using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.ClearingFlags
{
	public class ClearDecimalFlag : ClearFlagOpCode
	{
		public override string Name => "CLD";

		protected override CpuFlag Flag => CpuFlag.DecimalMode;

		public ClearDecimalFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}