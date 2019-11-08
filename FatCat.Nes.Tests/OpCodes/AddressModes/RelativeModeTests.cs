using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class RelativeModeTests : AddressModeTests
	{
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Relative";

		public RelativeModeTests() => addressMode = new Relative(cpu);

		[Fact]
		public void WillReadTheProgramCounterFromTheCpu()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillIncrementTheProgramCounter()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}
	}
}