using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class IndirectYModeTests : AddressModeTests
	{
		private const byte HighAddressValue = 0x19;

		private const byte InitialReadValue = 0xe2;

		private const byte LowAddressValue = 0x09;

		private const byte YRegister = 0xd1;

		private readonly ushort HighLocation = (InitialReadValue + 1) & 0x00ff;

		private readonly ushort LowLocation = InitialReadValue & 0x00ff;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "(Indirect),Y";

		public IndirectYModeTests()
		{
			addressMode = new IndirectYMode(cpu);

			cpu.YRegister = YRegister;

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(InitialReadValue);

			A.CallTo(() => cpu.Read(HighLocation)).Returns(HighAddressValue);
			A.CallTo(() => cpu.Read(LowLocation)).Returns(LowAddressValue);
		}

		[Fact]
		public void WillIncrementTheProgramCounter()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}

		[Fact]
		public void WillReadFromHighLocation()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(HighLocation)).MustHaveHappened();
		}

		[Fact]
		public void WillReadFromTheLowLocation()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(LowLocation)).MustHaveHappened();
		}

		[Fact]
		public void WillReadFromTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheAbsoluteAddressToValuesPlusTheYRegister()
		{
			addressMode.Run();

			ushort expectedAddress = (HighAddressValue << 8) | LowAddressValue;

			expectedAddress += YRegister;

			cpu.AbsoluteAddress.Should().Be(expectedAddress);
		}
	}
}