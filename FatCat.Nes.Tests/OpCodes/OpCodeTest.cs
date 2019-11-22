using FakeItEasy;
using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public abstract class OpCodeTest
	{
		private const byte FetchedData = 0x13;

		protected readonly IAddressMode addressMode;

		protected readonly ICpu cpu;

		protected OpCode opCode;

		protected abstract string ExpectedName { get; }

		protected OpCodeTest()
		{
			cpu = A.Fake<ICpu>();
			addressMode = A.Fake<IAddressMode>();

			A.CallTo(() => addressMode.Fetch()).Returns(FetchedData);
		}

		[Fact]
		public void NameWillBeThreeLetterOpCode() => opCode.Name.Should().Be(ExpectedName);
	}
}