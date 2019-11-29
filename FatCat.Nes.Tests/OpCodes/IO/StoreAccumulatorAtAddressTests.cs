using FakeItEasy;
using FatCat.Nes.OpCodes.IO;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class StoreAccumulatorAtAddressTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x1915;

		protected override string ExpectedName => "STA";

		public StoreAccumulatorAtAddressTests()
		{
			opCode = new StoreAccumulatorAtAddress(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void Takes0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WillWriteAccumulatorToAbsoluteAddress()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Write(AbsoluteAddress, Accumulator)).MustHaveHappened();
		}
	}
}