using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class IndirectXModeTests : AddressModeTests
	{
		private const byte HighAddressValue = 0x19;

		private const byte InitialReadValue = 0xe2;

		private const byte LowAddressValue = 0x09;

		private const byte XRegister = 0xd1;

		private readonly ushort HighLocation = (InitialReadValue + XRegister + 1) & 0x00ff;

		private readonly ushort LowLocation = (InitialReadValue + XRegister) & 0x00ff;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "(Indirect,X)";

		public IndirectXModeTests()
		{
			addressMode = new IndirectXMode(cpu);

			cpu.XRegister = XRegister;

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(InitialReadValue);

			A.CallTo(() => cpu.Read(LowLocation)).Returns(LowAddressValue);
			A.CallTo(() => cpu.Read(HighLocation)).Returns(HighAddressValue);
		}

		[Fact]
		public void AbsoluteAddressWillBeSetFromReadValues()
		{
			addressMode.Run();

			ushort expectedAddress = (HighAddressValue << 8) | LowAddressValue;

			cpu.AbsoluteAddress.Should().Be(expectedAddress);
		}

		[Fact]
		public void TheProgramCounterIsIncremented()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}

		[Fact]
		public void WillReadTheHighAddressUsingTheXRegister()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(HighLocation)).MustHaveHappened();
		}

		[Fact]
		public void WillReadTheLowAddressUsingTheXRegister()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(LowLocation)).MustHaveHappened();
		}

		[Fact]
		public void WillReadTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}