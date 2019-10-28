using FluentAssertions;
using Moq;
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

			bus.Verify(v => v.Write(It.IsAny<ushort>(), It.IsAny<byte>()), Times.Never);
			bus.Verify(v => v.Read(It.IsAny<ushort>()), Times.Never);
		}

		private void SetUpForDisableInterrupts()
		{
			bus.Reset();

			SetUpStartingCpuData();

			cpu.SetFlag(CpuFlag.DisableInterrupts);
		}
	}
}