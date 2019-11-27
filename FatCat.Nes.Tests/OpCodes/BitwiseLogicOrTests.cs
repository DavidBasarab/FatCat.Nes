using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BitwiseLogicOrTests : OpCodeTest
	{
		protected override string ExpectedName => "ORA";

		public BitwiseLogicOrTests() => opCode = new BitwiseLogicOr(cpu, addressMode);

		[Fact]
		public void WilLFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}