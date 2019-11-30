using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Transfers
{
	public class TransferAccumulatorToYRegister : TransferOpCode
	{
		public override string Name => "TAY";

		protected override byte TransferFromItem => cpu.Accumulator;

		protected override byte TransferTooItem
		{
			get => cpu.YRegister;
			set => cpu.YRegister = value;
		}

		public TransferAccumulatorToYRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}