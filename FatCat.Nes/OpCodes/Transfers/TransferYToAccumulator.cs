using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Transfers
{
	public class TransferYToAccumulator : TransferOpCode
	{
		public override string Name => "TYA";

		protected override byte TransferFromItem => cpu.YRegister;

		protected override byte TransferTooItem
		{
			get => cpu.Accumulator;
			set => cpu.Accumulator = value;
		}

		public TransferYToAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}