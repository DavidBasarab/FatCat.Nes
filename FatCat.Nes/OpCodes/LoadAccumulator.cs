using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class LoadAccumulator : OpCode
	{
		public override string Name => "LDA";

		public LoadAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			cpu.Accumulator = fetched;

			ApplyFlag(cpu.Accumulator.IsZero(), CpuFlag.Zero);
			ApplyFlag(cpu.Accumulator.IsNegative(), CpuFlag.Negative);

			return 1;
		}
	}
}