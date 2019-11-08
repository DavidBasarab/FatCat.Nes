using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ZeroPageYOffsetTests : AddressModeTests
	{
		private const byte YRegister = 0x3a;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "ZeroPage,Y";

		public ZeroPageYOffsetTests()
		{
			addressMode = new ZeroPageYOffset(cpu);

			cpu.YRegister = YRegister;
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

			ushort expectedAddress = (ReadValue + YRegister) & 0x00ff;

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