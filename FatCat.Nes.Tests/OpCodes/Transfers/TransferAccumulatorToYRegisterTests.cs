using FatCat.Nes.OpCodes.Transfers;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Transfers
{
	[UsedImplicitly]
	public class TransferAccumulatorToYRegisterTests : TransferTests
	{
		protected override byte CpuTransferFromItem
		{
			get => cpu.Accumulator;
			set => cpu.Accumulator = value;
		}

		protected override string ExpectedName => "TAY";

		protected override byte ExpectedValue => Accumulator;

		protected override byte CpuTransferItem => cpu.YRegister;

		public TransferAccumulatorToYRegisterTests() => opCode = new TransferAccumulatorToYRegister(cpu, addressMode);
	}
}