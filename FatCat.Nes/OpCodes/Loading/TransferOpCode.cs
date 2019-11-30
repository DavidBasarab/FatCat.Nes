using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public abstract class TransferOpCode : OpCode
	{
		protected abstract byte TransferFromItem { get; }

		protected abstract byte TransferTooItem { get; set; }

		protected TransferOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			TransferTooItem = TransferFromItem;

			ApplyFlag(CpuFlag.Zero, TransferTooItem.IsZero());
			ApplyFlag(CpuFlag.Negative, TransferTooItem.IsNegative());

			return 0;
		}
	}
}