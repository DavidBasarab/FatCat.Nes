using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class IndirectModeTests : AddressModeTests
	{
		private const byte HighPointer = 0xe1;
		private const byte LowPointer = 0x43;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Indirect";

		public IndirectModeTests()
		{
			addressMode = new IndirectMode(cpu);

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(LowPointer);
			A.CallTo(() => cpu.Read(ProgramCounter + 1)).Returns(HighPointer);
		}

		[Fact]
		public void WillReadLowPointer()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillReadHighPointer()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter + 1)).MustHaveHappened();
		}
	}
}