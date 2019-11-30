using FatCat.Nes.OpCodes.Transfers;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Transfers
{
	[UsedImplicitly]
	public class TransferYToAccumulatorTests : TransferTests
	{
		protected override byte CpuTransferFromItem
		{
			get => cpu.YRegister;
			set => cpu.YRegister = value;
		}

		protected override byte CpuTransferItem => cpu.Accumulator;

		protected override string ExpectedName => "TYA";

		protected override byte ExpectedValue => 0x77;

		public TransferYToAccumulatorTests() => opCode = new TransferYToAccumulator(cpu, addressMode);
	}
}