using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ReturnFromSubRoutineTests : OpCodeTest
	{
		private const byte HighProgramCounter = 0x2d;

		private const byte LowProgramCounter = 0x01;

		private const ushort ProgramCounter = 0x2d01;

		private const int StackPointer = 0xc2;

		protected override string ExpectedName => "RTS";

		public ReturnFromSubRoutineTests()
		{
			opCode = new ReturnFromSubRoutine(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillChangeStackPointerValueAfterReads()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(StackPointer + 2);
		}

		[Fact]
		public void WillReadFromStackPointerForHighMemory()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 2)).MustHaveHappened();
		}

		[Fact]
		public void WillReadFromStackPointerForLowMemory()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).MustHaveHappened();
		}
	}
}