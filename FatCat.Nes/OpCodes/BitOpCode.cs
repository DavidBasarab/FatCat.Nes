using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BitOpCode : OpCode
	{
		public override string Name => "BIT";

		public BitOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (ushort)(cpu.Accumulator & fetched);

			var binary = Convert.ToString(value, 2);

			ApplyFlag(value.ApplyLowMask() == 0x00, CpuFlag.Zero);

			return 0;
		}
	}
}