using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BranchIfCarrySetTests : OpCodeTest
	{
		private const ushort AbsoluteAddress = 0x4907;
		private const ushort ProgramCounter = 0x2019;

		protected override string ExpectedName => "BCS";

		public BranchIfCarrySetTests()
		{
			opCode = new BranchIfCarrySet(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
			cpu.ProgramCounter = ProgramCounter;

			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(true);
		}

		[Fact]
		public void WhenCarryFlagIsNotSetCyclesIsZero()
		{
			SetCarryFlagToFalse();

			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WhenCarryFlagIsNotSetNothingHappens()
		{
			SetCarryFlagToFalse();

			opCode.Execute();

			cpu.AbsoluteAddress.Should().Be(AbsoluteAddress);
			cpu.ProgramCounter.Should().Be(ProgramCounter);
		}

		[Fact]
		public void WillGetCarryFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		private void SetCarryFlagToFalse() { A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(false); }
	}
}