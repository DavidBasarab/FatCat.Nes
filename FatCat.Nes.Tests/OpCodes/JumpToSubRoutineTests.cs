using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class JumpToSubRoutineTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x91ab;
		private const int ProgramCounter = 0x6715;
		private const int StackPointer = 0x78;

		protected override string ExpectedName => "JSR";

		public JumpToSubRoutineTests()
		{
			opCode = new JumpToSubRoutine(cpu, addressMode);

			cpu.ProgramCounter = ProgramCounter;
			cpu.AbsoluteAddress = AbsoluteAddress;
			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillDecreaseStackPointer()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(StackPointer - 2);
		}

		[Fact]
		public void WillSetTheProgramCounterToTheAbsoluteAddress()
		{
			opCode.Execute();

			cpu.ProgramCounter.Should().Be(AbsoluteAddress);
		}

		[Fact]
		public void WillWriteToTheStackTheLowerMemoryPointer()
		{
			opCode.Execute();

			const byte expectedWriteValue = (ProgramCounter - 1) & 0x00ff;

			A.CallTo(() => cpu.Write(0x0100 + StackPointer - 1, expectedWriteValue)).MustHaveHappened();
		}

		[Fact]
		public void WillWriteToTheStackTheUpperMemoryPointer()
		{
			opCode.Execute();

			const byte expectedWriteValue = ((ProgramCounter - 1) >> 8) & 0x00ff;

			A.CallTo(() => cpu.Write(0x0100 + StackPointer, expectedWriteValue)).MustHaveHappened();
		}
	}
}