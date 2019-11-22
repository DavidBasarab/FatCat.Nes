using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AndTests : OpCodeTest
	{
		protected override string ExpectedName => "AND";

		public AndTests() => opCode = new And(cpu, addressMode);

		[Fact]
		public void WillFetchTheData()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheAccumulatorToBitWiseAndWithFetchedValue()
		{
			opCode.Execute();

			byte expectedValue = Accumulator & FetchedData;

			cpu.Accumulator.Should().Be(expectedValue);
		}
	}
}