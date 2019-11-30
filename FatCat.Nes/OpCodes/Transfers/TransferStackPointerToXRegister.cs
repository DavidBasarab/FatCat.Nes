using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Transfers
{
	public class TransferStackPointerToXRegister : TransferOpCode
	{
		public override string Name => "TSX";

		protected override byte TransferFromItem => cpu.StackPointer;

		protected override byte TransferTooItem
		{
			get => cpu.XRegister;
			set => cpu.XRegister = value;
		}

		public TransferStackPointerToXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}