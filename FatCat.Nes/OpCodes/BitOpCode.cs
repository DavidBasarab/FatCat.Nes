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

			ApplyFlag(value.ApplyLowMask() == 0x00, CpuFlag.Zero);

			ApplyFlag((fetched & (1 << 7)) > 0, CpuFlag.Negative);

			ApplyFlag((fetched & (1 << 6)) > 0, CpuFlag.Overflow);

			return 0;
		}
	}
}