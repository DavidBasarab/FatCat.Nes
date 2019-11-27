using FakeItEasy;
using FatCat.Nes.OpCodes;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BreakTests : OpCodeTest
	{
		private const ushort NewProgramCounter = 0x4215;
		private const byte NewProgramCounterHigh = 0x42;
		private const byte NewProgramCounterLow = 0x15;
		private const ushort ProgramCounter = 0x2817;
		private const ushort ProgramCounterHighLocation = 0xfffe;
		private const ushort ProgramCounterLowLocation = 0xffff;
		private const byte StackPointer = 0x32;

		protected override string ExpectedName => "BRK";

		public BreakTests()
		{
			opCode = new Break(cpu, addressMode);

			cpu.ProgramCounter = ProgramCounter;
			cpu.StackPointer = StackPointer;

			cpu.StatusRegister = CpuFlag.Break | CpuFlag.Zero | CpuFlag.DecimalMode | CpuFlag.DisableInterrupts;

			A.CallTo(() => cpu.Read(ProgramCounterLowLocation)).Returns(NewProgramCounterLow);
			A.CallTo(() => cpu.Read(ProgramCounterHighLocation)).Returns(NewProgramCounterHigh);
		}

		[Fact]
		public void BreakTakesNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WillReadHighMemoryPointer()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(ProgramCounterHighLocation)).MustHaveHappened();
		}

		[Fact]
		public void WillReadLowMemoryPointer()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(ProgramCounterLowLocation)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheBreakFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Break)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheInterruptFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.DisableInterrupts)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheProgramCounterToTheNewValue()
		{
			opCode.Execute();

			cpu.ProgramCounter.Should().Be(NewProgramCounter);
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
		public void WillWriteTheLowMemoryOfTheProgramCounter()
		{
			opCode.Execute();

			byte expectedProgramCounter = (ProgramCounter + 1) & 0x00ff;

			ushort writeAddress = StackPointer - 1 + 0x0100;

			A.CallTo(() => cpu.Write(writeAddress, expectedProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillWriteTheStackFlagToTheStack()
		{
			opCode.Execute();

			ushort expectedStackPointer = StackPointer - 2 + 0x0100;

			A.CallTo(() => cpu.Write(expectedStackPointer, (byte)cpu.StatusRegister)).MustHaveHappened();
		}
	}
}