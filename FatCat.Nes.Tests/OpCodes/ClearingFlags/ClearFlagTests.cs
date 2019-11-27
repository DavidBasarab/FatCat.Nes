using FakeItEasy;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.ClearingFlags
{
	public abstract class ClearFlagTests : OpCodeTest
	{
		protected abstract CpuFlag Flag { get; }

		[Fact]
		public void WillRemoveTheCarryFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(Flag)).MustHaveHappened();
		}

		[Fact]
		public void WillTakeNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}