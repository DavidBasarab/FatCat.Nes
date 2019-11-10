using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class IndirectModeXTests : AddressModeTests
	{
		private const byte HighAddressValue = 0x19;
		private const byte InitialReadValue = 0xe2;

		private const byte LowAddressValue = 0x09;

		private const byte XRegister = 0xd1;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "(Indirect,X)";

		public IndirectModeXTests()
		{
			addressMode = new IndirectModeX(cpu);

			cpu.XRegister = XRegister;

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(InitialReadValue);
		}

		[Fact]
		public void TheProgramCounterIsIncremented()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}

		[Fact]
		public void WillReadTheLowAddressUsingTheXRegister()
		{
			addressMode.Run();

			ushort expectedRead = (InitialReadValue + XRegister) & 0x00ff;

			A.CallTo(() => cpu.Read(expectedRead)).MustHaveHappened();
		}

		[Fact]
		public void WillReadTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}