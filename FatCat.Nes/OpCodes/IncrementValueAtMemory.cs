using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class IncrementValueAtMemory : OpCode
	{
		public override string Name => "INC";

		public IncrementValueAtMemory(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (ushort)(fetched + 1);

			cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			ApplyFlag(value.IsZero(), CpuFlag.Zero);
			ApplyFlag(value.IsNegative(), CpuFlag.Negative);

			return 0;
		}
	}
}