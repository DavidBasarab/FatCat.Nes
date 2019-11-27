using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class IncrementXRegister : OpCode
	{
		public override string Name => "INX";

		public IncrementXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.XRegister++;

			ApplyFlag(CpuFlag.Zero, cpu.XRegister.IsZero());
			ApplyFlag(CpuFlag.Negative, cpu.XRegister.IsNegative());

			return 0;
		}
	}
}