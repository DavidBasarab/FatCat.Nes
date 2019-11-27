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

			ApplyFlag(CpuFlag.CarryBit, RegisterValue >= fetched);

			ApplyFlag(CpuFlag.Zero, value.ApplyLowMask() == 0x0000);

			ApplyFlag(CpuFlag.Negative, value.IsNegative());

			return 1;
		}
	}
}