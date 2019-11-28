using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ReturnFromInterruptTests : OpCodeTest
	{
		private const byte HighProgramCounter = 0x3e;

		private const byte LowProgramCounter = 0x4f;

		private const ushort ProgramCounter = 0x3e4f;

		private const int StackPointer = 0xd3;

		private const CpuFlag StatusToReturn = CpuFlag.Negative | CpuFlag.Break | CpuFlag.CarryBit | CpuFlag.Overflow;

		protected override string ExpectedName => "RTI";

		public ReturnFromInterruptTests()
		{
			opCode = new ReturnFromInterrupt(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillIncreaseTheStackPointer()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(StackPointer + 3);
		}

		[Fact]
		public void WillReadFromTheStackPointer()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).MustHaveHappened();
		}

		[Fact]
		public void WillReadHighMemoryFromStack()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 3)).MustHaveHappened();
		}

		[Fact]
		public void WillReadLowMemoryFromStack()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 2)).MustHaveHappened();
		}

		[Fact]
		public void WillRemoveTheUnusedFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Unused)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheCpuStatusToStatusFoundOnStack()
		{
			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).Returns((byte)StatusToReturn);

			opCode.Execute();

			cpu.StatusRegister.Should().Be(StatusToReturn);
		}

		[Fact]
		public void WillSetTheProgramCounter()
		{
			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 2)).Returns(LowProgramCounter);
			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 3)).Returns(HighProgramCounter);

			opCode.Execute();

			cpu.ProgramCounter.Should().Be(ProgramCounter);
		}

		[Fact]
		public void WillTheyRemoveTheBreakFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Break)).MustHaveHappened();
		}
	}
}