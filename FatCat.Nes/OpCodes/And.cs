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
			
			ApplyFlag(cpu.Accumulator.IsZero(), CpuFlag.Zero);
			
			ApplyFlag(cpu.Accumulator.IsNegative(), CpuFlag.Negative);

			return 1;
		}
	}
}