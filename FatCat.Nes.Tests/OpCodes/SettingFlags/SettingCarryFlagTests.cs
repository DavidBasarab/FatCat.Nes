using FakeItEasy;
using FatCat.Nes.OpCodes.SettingFlags;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.SettingFlags
{
	public class SettingCarryFlagTests : OpCodeTest
	{
		protected override string ExpectedName => "SEC";

		public SettingCarryFlagTests() => opCode = new SettingCarryFlag(cpu, addressMode);

		[Fact]
		public void WillSetTheCarryFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}