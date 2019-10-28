using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public abstract class InterruptTests : CpuBaseTests
	{
		private const int EndingProgramCounter = 0x1121;

		protected const int StartingCpuCycles = 14;

		protected const int StartingProgramCounter = 0xd1b2;

		protected const int StartingStackPointer = 0xe1;

		protected abstract int InterruptCycles { get; }

		protected abstract ushort LowCounterLocation { get; }

		protected InterruptTests()
		{
			SetUpStartingCpuData();

			bus.Setup(v => v.Read(LowCounterLocation)).Returns(0x21);
			bus.Setup(v => v.Read((ushort)(LowCounterLocation + 1))).Returns(0x11);
		}

		[Fact]
		public void InterruptRequestTakesCycles() => cpu.Cycles.Should().Be(InterruptCycles);

		[Fact]
		public void WillPushTheStatusRegisterOnTheStack()
		{
			var expectedStatusRegister = CpuFlag.Negative | CpuFlag.Zero | CpuFlag.Unused | CpuFlag.DisableInterrupts;

			bus.Verify(v => v.Write(0x0100 + (StartingStackPointer - 2), (byte)expectedStatusRegister));
		}

		[Fact]
		public void WillReadHighCounterFromBus() => bus.Verify(v => v.Read((ushort)(LowCounterLocation + 1)));

		[Fact]
		public void WillReadLowCounterFromBus() => bus.Verify(v => v.Read(LowCounterLocation));

		[Fact]
		public void WillReduceTheStackPointer() => cpu.StackPointer.Should().Be(0xde);

		[Fact]
		public void WillSetTheProgramCounterFromValuesReadFromBus() => cpu.ProgramCounter.Should().Be(EndingProgramCounter);

		[Fact]
		public void WillWriteHighMemoryToStack() => bus.Verify(v => v.Write(0x0100 + StartingStackPointer, 0xd1));

		[Fact]
		public void WillWriteLowMemoryToStack() => bus.Verify(v => v.Write(0x0100 + (StartingStackPointer - 1), 0xb2));

		protected void SetUpStartingCpuData()
		{
			cpu.StackPointer = StartingStackPointer;
			cpu.ProgramCounter = StartingProgramCounter;
			cpu.StatusRegister = CpuFlag.Negative | CpuFlag.Zero | CpuFlag.Break;
			cpu.Cycles = StartingCpuCycles;
		}
	}
}