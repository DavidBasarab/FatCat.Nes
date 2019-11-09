using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AbsoluteXOffsetModeTests : AbsoluteModeTests
	{
		private const byte XRegister = 0x01;

		protected override string ExpectedName => "Absolute,X";

		public AbsoluteXOffsetModeTests()
		{
			addressMode = new AbsoluteXOffset(cpu);

			cpu.XRegister = XRegister;
		}

		[Fact]
		public void IfTheXRegisterCauseTheAPageJumpThenAnAdditionalCycleIsNeeded()
		{
			cpu.XRegister = 0xfe;

			var cycles = addressMode.Run();

			cycles.Should().Be(1);
		}

		[Fact]
		public void WillAddXRegisterToAbsoluteAddress()
		{
			addressMode.Run();

			cpu.AbsoluteAddress.Should().Be(0xe43e);
		}
	}
}