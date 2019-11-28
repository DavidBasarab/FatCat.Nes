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

			var value = (GetCarryFlagValue() << 7) | (fetched >> 1);

			if (ImpliedAddressMode) cpu.Accumulator = value.ApplyLowMask();
			else cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			return -1;
		}
	}
}