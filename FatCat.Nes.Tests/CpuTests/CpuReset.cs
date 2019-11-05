using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuReset : CpuBaseTests
	{
		private const int ClockCount = 9003;
		private const int OpCode = 0xb1;

		public CpuReset()
		{
			cpu.AbsoluteAddress = 0x2010;
			cpu.RelativeAddress = 0x1191;
			cpu.Accumulator = 0x78;
			cpu.XRegister = 0x12;
			cpu.YRegister = 0x56;
			cpu.StackPointer = 0xF3;
			cpu.StatusRegister = CpuFlag.Break | CpuFlag.Overflow | CpuFlag.CarryBit;
			cpu.ProgramCounter = 0x7834;
			cpu.Cycles = 3482;
			cpu.ClockCount = ClockCount;
			cpu.Fetched = 0x67;
			cpu.OpCode = OpCode;
		}

		[Fact]
		public void ClockCountWillNotChange()
		{
			cpu.Reset();

			cpu.ClockCount.Should().Be(ClockCount);
		}

		[Fact]
		public void FetchedWillBeSetToZero()
		{
			cpu.Reset();

			cpu.Fetched.Should().Be(0x00);
		}

		[Fact]
		public void OpCodeWillNotChange()
		{
			cpu.Reset();

			cpu.OpCode.Should().Be(OpCode);
		}

		[Fact]
		public void ResetTakes8Cycles()
		{
			cpu.Reset();

			cpu.Cycles.Should().Be(8);
		}

		[Fact]
		public void WillReadHighAddress()
		{
			cpu.Reset();

			A.CallTo(() => bus.Read(0xfffd)).MustHaveHappened();
		}

		[Fact]
		public void WillReadLowAddress()
		{
			cpu.Reset();

			A.CallTo(() => bus.Read(0xfffc)).MustHaveHappened();
		}

		[Fact]
		public void WillResetYRegister()
		{
			cpu.Reset();

			cpu.YRegister.Should().Be(0);
		}

		[Fact]
		public void WillSetAccumulatorTo0()
		{
			cpu.Reset();

			cpu.Accumulator.Should().Be(0);
		}

		[Fact]
		public void WillSetProgramCounterToAddressRead()
		{
			byte lowAddress = 0x11;
			byte highAddress = 0x22;

			A.CallTo(() => bus.Read(0xfffc)).Returns(lowAddress);
			A.CallTo(() => bus.Read(0xfffd)).Returns(highAddress);

			cpu.Reset();

			cpu.ProgramCounter.Should().Be(0x2211);
		}

		[Fact]
		public void WillSetRelativeAddressTo0()
		{
			cpu.Reset();

			cpu.RelativeAddress.Should().Be(0x0000);
		}

		[Fact]
		public void WillSetStatusRegisterToUnused()
		{
			cpu.Reset();

			cpu.StatusRegister.Should().HaveFlag(CpuFlag.Unused);
		}

		[Fact]
		public void WIllSetTheAbsoluteAddressTo0()
		{
			cpu.Reset();

			cpu.AbsoluteAddress.Should().Be(0x0000);
		}

		[Fact]
		public void WillSetTheStackPointTo253()
		{
			cpu.Reset();

			cpu.StackPointer.Should().Be(0xfd);
		}

		[Fact]
		public void WillSetXRegisterToZero()
		{
			cpu.Reset();

			cpu.XRegister.Should().Be(0);
		}
	}
}