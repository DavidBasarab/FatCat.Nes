using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class DecrementXRegister : OpCode
	{
		public override string Name => "DEX";

		public DecrementXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.XRegister--;

			ApplyFlag(CpuFlag.Zero, cpu.XRegister.IsZero());
			ApplyFlag(CpuFlag.Negative, cpu.XRegister.IsNegative());

			return 0;
		}
	}
}