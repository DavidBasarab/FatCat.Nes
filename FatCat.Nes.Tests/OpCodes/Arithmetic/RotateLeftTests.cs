using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class RotateLeftTests : OpCodeTest
	{
		protected override string ExpectedName => "ROL";

		public RotateLeftTests() => opCode = new RotateLeft(cpu, addressMode);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}