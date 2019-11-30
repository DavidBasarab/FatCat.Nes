using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public class TransferAccumulatorToXRegister : OpCode
	{
		public override string Name => "TAX";

		public TransferAccumulatorToXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.XRegister = cpu.Accumulator;

			ApplyFlag(CpuFlag.Zero, cpu.XRegister.IsZero());
			ApplyFlag(CpuFlag.Negative, cpu.XRegister.IsNegative());

			return 0;
		}
	}
}