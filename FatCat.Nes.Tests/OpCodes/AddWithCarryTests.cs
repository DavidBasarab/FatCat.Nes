using FakeItEasy;
using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AddWithCarryTests
	{
		private const ushort AbsoluteAddress = 0x2807;
		private const byte FetchedData = 0x13;
		private readonly IAddressMode addressMode;
		private readonly ICpu cpu;
		private readonly AddWithCarry opCode;

		public AddWithCarryTests()
		{
			cpu = A.Fake<ICpu>();

			cpu.AbsoluteAddress = AbsoluteAddress;

			A.CallTo(() => cpu.Read(AbsoluteAddress)).Returns(FetchedData);

			addressMode = A.Fake<IAddressMode>();

			opCode = new AddWithCarry(cpu, addressMode);
		}

		[Fact]
		public void WillReadFromTheAbsoluteAddress()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(AbsoluteAddress)).MustHaveHappened();
		}
	}
}