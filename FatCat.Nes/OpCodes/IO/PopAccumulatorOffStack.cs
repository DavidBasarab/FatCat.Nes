using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PopAccumulatorOffStack : OpCode
	{
		public override string Name => "PLA";

		public PopAccumulatorOffStack(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.Accumulator = ReadFromStack();

			ApplyFlag(CpuFlag.Zero, cpu.Accumulator.IsZero());
			ApplyFlag(CpuFlag.Negative, cpu.Accumulator.IsNegative());

			return 0;
		}
	}
}