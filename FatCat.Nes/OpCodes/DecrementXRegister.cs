using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class DecrementXRegister : OpCode
	{
		public override string Name => "DEX";

		public DecrementXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.XRegister--;

			cpu.Write(cpu.AbsoluteAddress, cpu.XRegister.ApplyLowMask());

			ApplyFlag(cpu.XRegister.IsZero(), CpuFlag.Zero);
			ApplyFlag(cpu.XRegister.IsNegative(), CpuFlag.Negative);

			return 0;
		}
	}
}