using FluentAssertions;
using Moq;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class InterruptRequest : CpuBaseTests
	{
		private const int EndingProgramCounter = 0x1121;
		private const int StartingCpuCycles = 14;

		private const int StartingProgramCounter = 0xd1b2;

		private const int StartingStackPointer = 0xe1;

		public InterruptRequest()
		{
			SetUpStartingCpuData();

			bus.Setup(v => v.Read(0xfffe)).Returns(0x21);
			bus.Setup(v => v.Read(0xffff)).Returns(0x11);

			cpu.Irq();
		}

		[Fact]
		public void IfFlagDisableInterruptIsSetThenNoCpuDataIsChanged()
		{
			SetUpForDisableInterrupts();

			cpu.Irq();

			cpu.ProgramCounter.Should().Be(StartingProgramCounter);
			cpu.StackPointer.Should().Be(StartingStackPointer);
			cpu.Cycles.Should().Be(StartingCpuCycles);
		}

		[Fact]
		public void IfFlagDisableInterruptIsSetThenNoReadsAndWritesAreDoneToTheBus()
		{
			SetUpForDisableInterrupts();

			cpu.Irq();

			bus.Verify(v => v.Write(It.IsAny<ushort>(), It.IsAny<byte>()), Times.Never);
			bus.Verify(v => v.Read(It.IsAny<ushort>()), Times.Never);
		}

		[Fact]
		public void InterruptRequestTakesCycles() => cpu.Cycles.Should().Be(7);

		[Fact]
		public void WillPushTheStatusRegisterOnTheStack()
		{
			var expectedStatusRegister = CpuFlag.Negative | CpuFlag.Zero | CpuFlag.Unused | CpuFlag.DisableInterrupts;

			bus.Verify(v => v.Write(0x0100 + (StartingStackPointer - 2), (byte)expectedStatusRegister));
		}

		[Fact]
		public void WillReadHighCounterFromBus() => bus.Verify(v => v.Read(0xffff));

		[Fact]
		public void WillReadLowCounterFromBus() => bus.Verify(v => v.Read(0xfffe));

		[Fact]
		public void WillReduceTheStackPointer() => cpu.StackPointer.Should().Be(0xde);

		[Fact]
		public void WillSetTheProgramCounterFromValuesReadFromBus() => cpu.ProgramCounter.Should().Be(EndingProgramCounter);

		[Fact]
		public void WillWriteHighMemoryToStack() => bus.Verify(v => v.Write(0x0100 + StartingStackPointer, 0xd1));

		[Fact]
		public void WillWriteLowMemoryToStack() => bus.Verify(v => v.Write(0x0100 + (StartingStackPointer - 1), 0xb2));

		private void SetUpForDisableInterrupts()
		{
			bus.Reset();

			SetUpStartingCpuData();

			cpu.SetFlag(CpuFlag.DisableInterrupts);
		}

		private void SetUpStartingCpuData()
		{
			cpu.StackPointer = StartingStackPointer;
			cpu.ProgramCounter = StartingProgramCounter;
			cpu.StatusRegister = CpuFlag.Negative | CpuFlag.Zero | CpuFlag.Break;
			cpu.Cycles = StartingCpuCycles;
		}
	}
}