using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class IllegalOpCodeTests : OpCodeTest
	{
		protected override string ExpectedName => "XXX";

		public IllegalOpCodeTests() => opCode = new IllegalOpCode(cpu, addressMode);

		[Fact]
		public void WillDoNothingIn0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}