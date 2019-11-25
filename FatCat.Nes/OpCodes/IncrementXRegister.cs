using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class IncrementXRegister : OpCode
	{
		public override string Name => "INX";

		public IncrementXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.XRegister++;

			ApplyFlag(cpu.XRegister.IsZero(), CpuFlag.Zero);
			ApplyFlag(cpu.XRegister.IsNegative(), CpuFlag.Negative);

			return 0;
		}
	}
}