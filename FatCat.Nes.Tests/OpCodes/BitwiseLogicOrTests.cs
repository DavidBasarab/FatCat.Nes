using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BitwiseLogicOrTests : OpCodeTest
	{
		protected override string ExpectedName => "ORA";

		public BitwiseLogicOrTests() => opCode = new BitwiseLogicOr(cpu, addressMode);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillOrTheAccumulatorWithMemoryData()
		{
			opCode.Execute();

			byte expectedValue = Accumulator | FetchedData;

			cpu.Accumulator.Should().Be(expectedValue);
		}
	}
}