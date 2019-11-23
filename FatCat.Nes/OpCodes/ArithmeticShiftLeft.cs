using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ArithmeticShiftLeft : OpCode
	{
		public override string Name => "ASL";

		public ArithmeticShiftLeft(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var shiftValue = (ushort)(fetched << 1);

			ApplyFlag(shiftValue.HasCarried(), CpuFlag.CarryBit);

			ApplyFlag(shiftValue.ApplyLowMask() == 0x00, CpuFlag.Zero);

			return -1;
		}
	}
}