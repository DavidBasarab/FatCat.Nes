using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PopAccumulatorOffStack : OpCode
	{
		public override string Name => "PLA";

		public PopAccumulatorOffStack(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.StackPointer++;

			cpu.Accumulator = cpu.Read((ushort)(0x0100 + cpu.StackPointer));
			
			ApplyFlag(CpuFlag.Zero, cpu.Accumulator.IsZero());
			ApplyFlag(CpuFlag.Negative, cpu.Accumulator.IsNegative());

			return 0;
		}
	}
}