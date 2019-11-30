using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Transfers
{
	public class TransferXRegisterToAccumulator : TransferOpCode
	{
		public override string Name => "TXA";

		protected override byte TransferFromItem => cpu.XRegister;

		protected override byte TransferTooItem
		{
			get => cpu.Accumulator;
			set => cpu.Accumulator = value;
		}

		public TransferXRegisterToAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}