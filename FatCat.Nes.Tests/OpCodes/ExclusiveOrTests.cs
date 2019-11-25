using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ExclusiveOrTests : OpCodeTest
	{
		protected override string ExpectedName => "EOR";

		public ExclusiveOrTests() => opCode = new ExclusiveOr(cpu, addressMode);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillDoXorOnAccumulator()
		{
			opCode.Execute();

			byte expectedAccumulator = Accumulator ^ FetchedData;

			cpu.Accumulator.Should().Be(expectedAccumulator);
		}
	}
}