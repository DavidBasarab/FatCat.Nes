using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class InterruptRequest : InterruptTests
	{
		protected override int InterruptCycles => 7;

		protected override ushort LowCounterLocation => 0xfffe;

		public InterruptRequest() => cpu.Irq();

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

			A.CallTo(() => bus.Write(A<ushort>.Ignored, A<byte>.Ignored)).MustNotHaveHappened();
			A.CallTo(() => bus.Read(A<ushort>.Ignored)).MustNotHaveHappened();
		}

		private void SetUpForDisableInterrupts()
		{
			Fake.ClearRecordedCalls(bus);

			SetUpStartingCpuData();

			cpu.SetFlag(CpuFlag.DisableInterrupts);
		}
	}
}