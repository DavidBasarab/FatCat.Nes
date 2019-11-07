using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ZeroPageModeTests : AddressModeTests
	{
		private const int ProgramCounter = 0xEF12;

		protected override string ExpectedName => "ZeroPage";

		public ZeroPageModeTests()
		{
			addressMode = new ZeroPageMode(cpu);

			cpu.ProgramCounter = ProgramCounter;
		}

		[Fact]
		public void WillReadFromCpuAtTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}