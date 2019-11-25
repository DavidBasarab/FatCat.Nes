using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class DecrementYRegister : OpCode
	{
		public override string Name => "DEY";

		public DecrementYRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.YRegister--;

			ApplyFlag(cpu.YRegister.IsZero(), CpuFlag.Zero);
			ApplyFlag(cpu.YRegister.IsNegative(), CpuFlag.Negative);

			return 0;
		}
	}
}