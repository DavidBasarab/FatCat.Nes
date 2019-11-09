using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AbsoluteModeTests : AddressModeTests
	{
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Absolute";

		public AbsoluteModeTests() => addressMode = new Absolute(cpu);

		[Fact]
		public void WillReadFromStartingProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}