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

		private const byte XRegister = 0xd1;

		private readonly ushort HighLocation = (InitialReadValue + XRegister + 1) & 0x00ff;

		private readonly ushort LowLocation = (InitialReadValue + XRegister) & 0x00ff;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "(Indirect),Y";

		public IndirectYModeTests()
		{
			addressMode = new IndirectYMode(cpu);

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(InitialReadValue);
		}

		[Fact]
		public void WillIncrementTheProgramCounter()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}

		[Fact]
		public void WillReadFromTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}