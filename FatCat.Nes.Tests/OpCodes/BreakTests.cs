using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BreakTests : OpCodeTest
	{
		private const ushort ProgramCounter = 0x2817;
		private const byte StackPointer = 0x32;

		protected override string ExpectedName => "BRK";

		public BreakTests()
		{
			opCode = new Break(cpu, addressMode);

			cpu.ProgramCounter = ProgramCounter;
			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillSetTheInterruptFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.DisableInterrupts)).MustHaveHappened();
		}

		[Fact]
		public void WillWriteTheHighMemoryProgramCounter()
		{
			opCode.Execute();

			byte expectedProgramCounter = ((ProgramCounter + 1) >> 8) & 0x00ff;

			ushort writeAddress = StackPointer + 0x0100;

			A.CallTo(() => cpu.Write(writeAddress, expectedProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WIllWriteTheLowMemoryOfTheProgramCounter()
		{
			opCode.Execute();

			byte expectedProgramCounter = (ProgramCounter + 1) & 0x00ff;

			ushort writeAddress = StackPointer - 1 + 0x0100;

			A.CallTo(() => cpu.Write(writeAddress, expectedProgramCounter)).MustHaveHappened();
		}
	}
}