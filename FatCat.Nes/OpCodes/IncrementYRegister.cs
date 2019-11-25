using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class IncrementYRegister : OpCode
	{
		public override string Name => "INY";

		public IncrementYRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.YRegister++;

			ApplyFlag(cpu.YRegister.IsZero(), CpuFlag.Zero);
			ApplyFlag(cpu.YRegister.IsNegative(), CpuFlag.Negative);

			return 0;
		}
	}
}