using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Transfers
{
	public class TransferAccumulatorToXRegister : TransferOpCode
	{
		public override string Name => "TAX";

		protected override byte TransferFromItem => cpu.Accumulator;

		protected override byte TransferTooItem
		{
			get => cpu.XRegister;
			set => cpu.XRegister = value;
		}

		public TransferAccumulatorToXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}