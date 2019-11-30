using FatCat.Nes.OpCodes.Transfers;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Transfers
{
	public class TransferXRegisterToStackPointerTests : OpCodeTest
	{
		private const int XRegister = 0x98;

		protected override string ExpectedName => "TXS";

		public TransferXRegisterToStackPointerTests()
		{
			opCode = new TransferXRegisterToStackPointer(cpu, addressMode);

			cpu.XRegister = XRegister;
		}

		[Fact]
		public void WillSetStackPointerToXRegisterValue()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(XRegister);
		}

		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}