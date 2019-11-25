using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Comparing
{
	public abstract class CompareOpcode : OpCode
	{
		protected abstract byte RegisterValue { get; }

		protected CompareOpcode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (ushort)(RegisterValue - fetched);

			ApplyFlag(RegisterValue >= fetched, CpuFlag.CarryBit);

			ApplyFlag(value.ApplyLowMask() == 0x0000, CpuFlag.Zero);

			ApplyFlag(value.IsNegative(), CpuFlag.Negative);

			return 1;
		}
	}
}