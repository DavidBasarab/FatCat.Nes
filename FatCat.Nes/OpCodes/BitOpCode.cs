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

			ApplyFlag(CpuFlag.Zero, value.ApplyLowMask() == 0x00);

			ApplyFlag(CpuFlag.Negative, (fetched & (1 << 7)) > 0);

			ApplyFlag(CpuFlag.Overflow, (fetched & (1 << 6)) > 0);

			return 0;
		}
	}
}