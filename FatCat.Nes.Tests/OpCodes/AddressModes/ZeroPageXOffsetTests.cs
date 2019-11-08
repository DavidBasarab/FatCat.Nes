using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ZeroPageXOffsetTests : AddressModeTests
	{
		private const byte XRegister = 0xd1;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "ZeroPage,X";

		public ZeroPageXOffsetTests()
		{
			addressMode = new ZeroPageXOffset(cpu);

			cpu.XRegister = XRegister;
		}

		[Fact]
		public void ItWillReadFromTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillAddReadValueAndXRegisterToAbsoluteAddress()
		{
			addressMode.Run();

			ushort expectedAddress = (ReadValue + XRegister) & 0x00ff;

			cpu.AbsoluteAddress.Should().Be(expectedAddress);
		}

		[Fact]
		public void WillIncrementTheProgramCounter()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}
	}
}