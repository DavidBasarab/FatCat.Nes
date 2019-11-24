using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.ClearingFlags
{
	public class ClearCarryFlag : ClearFlagOpCode
	{
		public override string Name => "CLC";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		public ClearCarryFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}