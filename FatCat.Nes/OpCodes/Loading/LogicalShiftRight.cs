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

			ApplyFlag(CpuFlag.CarryBit, (fetched & 0x0001) > 0);

			var value = (ushort)(fetched >> 1);

			ApplyFlag(CpuFlag.Zero, value.ApplyLowMask().IsZero());
			ApplyFlag(CpuFlag.Negative, value.IsNegative());

			if (ImpliedAddressMode) cpu.Accumulator = value.ApplyLowMask();
			else cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			return 0;
		}
	}
}