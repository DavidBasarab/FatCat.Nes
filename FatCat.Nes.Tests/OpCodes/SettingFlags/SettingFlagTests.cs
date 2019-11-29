using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.SettingFlags
{
	public abstract class SettingFlagTests : OpCodeTest
	{
		protected abstract CpuFlag Flag { get; }

		[Fact]
		public void WillSetTheCarryFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(Flag)).MustHaveHappened();
		}

		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}