using FatCat.Nes.OpCodes.Loading;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	[UsedImplicitly]
	public class TransferAccumulatorToXRegisterTests : TransferTests
	{
		protected override byte CpuTransferFromItem
		{
			get => cpu.Accumulator;
			set => cpu.Accumulator = value;
		}

		protected override string ExpectedName => "TAX";

		protected override byte ExpectedValue => Accumulator;

		protected override byte CpuTransferItem => cpu.XRegister;

		public TransferAccumulatorToXRegisterTests() => opCode = new TransferAccumulatorToXRegister(cpu, addressMode);
	}
}