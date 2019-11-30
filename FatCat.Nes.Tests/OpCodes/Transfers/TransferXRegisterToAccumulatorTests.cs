using FatCat.Nes.OpCodes.Transfers;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Transfers
{
	[UsedImplicitly]
	public class TransferXRegisterToAccumulatorTests : TransferTests
	{
		protected override byte CpuTransferFromItem
		{
			get => cpu.XRegister;
			set => cpu.XRegister = value;
		}

		protected override byte CpuTransferItem => cpu.Accumulator;

		protected override string ExpectedName => "TXA";

		protected override byte ExpectedValue => 0x87;

		public TransferXRegisterToAccumulatorTests() => opCode = new TransferXRegisterToAccumulator(cpu, addressMode);
	}
}