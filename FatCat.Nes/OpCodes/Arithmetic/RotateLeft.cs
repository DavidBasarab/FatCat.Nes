using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class RotateLeft : OpCode
	{
		public override string Name => "ROL";

		public RotateLeft(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (fetched << 1) | GetCarryFlagValue();

			ApplyFlag(CpuFlag.CarryBit, value.ApplyHighMask() > 0);
			ApplyFlag(CpuFlag.Zero, value.ApplyLowMask().IsZero());
			ApplyFlag(CpuFlag.Negative, value.IsNegative());

			if (ImpliedAddressMode) cpu.Accumulator = value.ApplyLowMask();
			else cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			return -1;
		}
	}
}