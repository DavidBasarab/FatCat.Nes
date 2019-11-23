using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BranchIfEqualTests : OpCodeTest
	{
		private const ushort AbsoluteAddress = 0x4907;
		private const ushort ProgramCounter = 0x2019;

		public static IEnumerable<object[]> BranchData
		{
			[UsedImplicitly]
			get
			{
				// Will not move the page
				yield return new object[]
							{
								0x1102, // Program Counter
								0x0003, // Relative Address,
								1       // Cycles
							};

				// Will move the page
				yield return new object[]
							{
								0x17d2, // Program Counter
								0x17FF, // Relative Address,
								2       // Cycles
							};
			}
		}

		protected override string ExpectedName => "BEQ";

		public BranchIfEqualTests()
		{
			opCode = new BranchIfEqual(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
			cpu.ProgramCounter = ProgramCounter;

			A.CallTo(() => cpu.GetFlag(CpuFlag.Zero)).Returns(true);
		}

		[Fact]
		public void WhenCarryFlagIsNotSetCyclesIsZero()
		{
			SetZeroFlagToFalse();

			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WhenCarryFlagIsNotSetNothingHappens()
		{
			SetZeroFlagToFalse();

			opCode.Execute();

			cpu.AbsoluteAddress.Should().Be(AbsoluteAddress);
			cpu.ProgramCounter.Should().Be(ProgramCounter);
		}

		[Theory]
		[MemberData(nameof(BranchData), MemberType = typeof(BranchIfEqualTests))]
		public void WillBranchBasedOnProvidedData(ushort programCounter, ushort relativeAddress, int expectedCycles)
		{
			cpu.ProgramCounter = programCounter;
			cpu.RelativeAddress = relativeAddress;

			var cycles = opCode.Execute();

			var expectedAbsoluteAddress = (ushort)(programCounter + relativeAddress);

			cpu.AbsoluteAddress.Should().Be(expectedAbsoluteAddress);
			cpu.ProgramCounter.Should().Be(expectedAbsoluteAddress);

			cycles.Should().Be(expectedCycles);
		}

		[Fact]
		public void WillGetZeroFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.GetFlag(CpuFlag.Zero)).MustHaveHappened();
		}

		private void SetZeroFlagToFalse() => A.CallTo(() => cpu.GetFlag(CpuFlag.Zero)).Returns(false);
	}
}