using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class RotateRight : OpCode
	{
		public override string Name => "ROR";

		public RotateRight(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			ushort value = (ushort)((GetCarryFlagValue() << 7) | (fetched >> 1));

			ApplyFlag(CpuFlag.CarryBit, (fetched & 0x01) > 0);
			ApplyFlag(CpuFlag.Zero, value.ApplyLowMask().IsZero());
			ApplyFlag(CpuFlag.Negative, value.IsNegative());

			if (ImpliedAddressMode) cpu.Accumulator = value.ApplyLowMask();
			else cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			return -1;
		}
	}
}