using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public class LogicalShiftRight : OpCode
	{
		public override string Name => "LSR";

		public LogicalShiftRight(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			ApplyFlag((fetched & 0x0001) > 0, CpuFlag.CarryBit);

			var value = (ushort)(fetched >> 1);

			ApplyFlag(value.ApplyLowMask().IsZero(), CpuFlag.Zero);
			ApplyFlag(value.IsNegative(), CpuFlag.Negative);

			return -1;
		}
	}
}