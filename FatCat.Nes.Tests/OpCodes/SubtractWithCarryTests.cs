using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class SubtractWithCarryTests : OpCodeTest
	{
		public SubtractWithCarryTests() => opCode = new SubtractWithCarry(cpu, addressMode);
		
		[Fact]
		public void NameWillBeSBC() => opCode.Name.Should().Be("SBC");

		[Fact]
		public void WillFetchTheData()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}