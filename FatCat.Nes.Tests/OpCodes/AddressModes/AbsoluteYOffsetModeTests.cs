using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AbsoluteYOffsetModeTests : AbsoluteModeTests
	{
		private const byte YRegister = 0x01;

		protected override string ExpectedName => "Absolute,Y";

		public AbsoluteYOffsetModeTests()
		{
			addressMode = new AbsoluteYOffset(cpu);

			cpu.YRegister = YRegister;
		}

		[Fact]
		public void IfTheXRegisterCauseTheAPageJumpThenAnAdditionalCycleIsNeeded()
		{
			cpu.YRegister = 0xfe;

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