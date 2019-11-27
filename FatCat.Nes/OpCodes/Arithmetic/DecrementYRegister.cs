using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class DecrementYRegister : OpCode
	{
		public override string Name => "DEY";

		public DecrementYRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.YRegister--;

			ApplyFlag(CpuFlag.Zero, cpu.YRegister.IsZero());
			ApplyFlag(CpuFlag.Negative, cpu.YRegister.IsNegative());

			return 0;
		}
	}
}