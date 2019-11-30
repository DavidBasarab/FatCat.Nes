using FatCat.Nes.OpCodes.Loading;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	public class TransferAccumulatorToXRegisterTests : OpCodeTest
	{
		protected override string ExpectedName => "TAX";

		public TransferAccumulatorToXRegisterTests()
		{
			opCode = new TransferAccumulatorToXRegister(cpu, addressMode);

			cpu.XRegister = 0x49;
		}

		[Fact]
		public void WillSetTheXRegisterToAccumulatorValue()
		{
			opCode.Execute();

			cpu.XRegister.Should().Be(Accumulator);
		}
	}
}