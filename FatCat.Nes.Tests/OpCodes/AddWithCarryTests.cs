using FakeItEasy;
using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AddWithCarryTests
	{
		private const byte FetchedData = 0x13;

		private readonly IAddressMode addressMode;

		private readonly ICpu cpu;

		private readonly AddWithCarry opCode;

		public AddWithCarryTests()
		{
			cpu = A.Fake<ICpu>();
			addressMode = A.Fake<IAddressMode>();

			A.CallTo(() => addressMode.Fetch()).Returns(FetchedData);

			opCode = new AddWithCarry(cpu, addressMode);
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}