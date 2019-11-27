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

			ApplyFlag(CpuFlag.CarryBit, shiftValue.HasCarried());

			ApplyFlag(CpuFlag.Zero, shiftValue.ApplyLowMask() == 0x00);

			ApplyFlag(CpuFlag.Negative, shiftValue.IsNegative());

			if (addressMode.Name == "Implied") cpu.Accumulator = shiftValue.ApplyLowMask();
			else cpu.Write(cpu.AbsoluteAddress, shiftValue.ApplyLowMask());

			return 0;
		}
	}
}