using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class DecrementValueAtMemory : OpCode
	{
		public override string Name => "DEC";

		public DecrementValueAtMemory(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (ushort)(fetched - 1);

			cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			ApplyFlag(CpuFlag.Zero, value.IsZero());
			ApplyFlag(CpuFlag.Negative, value.IsNegative());

			return 0;
		}
	}
}