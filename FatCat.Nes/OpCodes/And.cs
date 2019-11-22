using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class And : OpCode
	{
		public override string Name => "AND";

		public And(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			cpu.Accumulator = (byte)(cpu.Accumulator & fetched);

			if (cpu.Accumulator.IsZero()) cpu.SetFlag(CpuFlag.Zero);
			else cpu.RemoveFlag(CpuFlag.Zero);

			if (cpu.Accumulator.IsNegative()) cpu.SetFlag(CpuFlag.Negative);
			else cpu.RemoveFlag(CpuFlag.Negative);

			return 1;
		}
	}
}