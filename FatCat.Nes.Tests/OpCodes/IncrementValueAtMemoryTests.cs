using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class IncrementValueAtMemoryTests : OpCodeTest
	{
		protected override string ExpectedName => "INC";

		public IncrementValueAtMemoryTests() => opCode = new IncrementValueAtMemory(cpu, addressMode);

		[Fact]
		public void WillFetchTheMemoryFromAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}