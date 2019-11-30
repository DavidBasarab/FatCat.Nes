using FatCat.Nes.OpCodes.Transfers;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Transfers
{
	[UsedImplicitly]
	public class TransferStackPointerToXRegisterTests : TransferTests
	{
		protected override byte CpuTransferFromItem
		{
			get => cpu.StackPointer;
			set => cpu.StackPointer = value;
		}

		protected override byte CpuTransferItem => cpu.XRegister;

		protected override string ExpectedName => "TSX";

		protected override byte ExpectedValue => 0x32;

		public TransferStackPointerToXRegisterTests() => opCode = new TransferStackPointerToXRegister(cpu, addressMode);
	}
}