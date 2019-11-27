using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Branching
{
	public abstract class BranchTests : OpCodeTest
	{
		private const ushort AbsoluteAddress = 0x4907;
		private const ushort ProgramCounter = 0x2019;

		public static IEnumerable<object[]> BranchCarryData
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

		protected abstract CpuFlag Flag { get; }

		protected abstract bool FlagState { get; }

		protected BranchTests()
		{
			cpu.AbsoluteAddress = AbsoluteAddress;
			cpu.ProgramCounter = ProgramCounter;

			// ReSharper disable once VirtualMemberCallInConstructor
			A.CallTo(() => cpu.GetFlag(Flag)).Returns(FlagState);
		}

		[Fact]
		public void WhenFlagIsNotSetNothingHappens()
		{
			SwitchFlagState();

			opCode.Execute();

			cpu.AbsoluteAddress.Should().Be(AbsoluteAddress);
			cpu.ProgramCounter.Should().Be(ProgramCounter);
		}

		[Fact]
		public void WhenFlagIsValidCyclesIsZero()
		{
			SwitchFlagState();

			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Theory]
		[MemberData(nameof(BranchCarryData), MemberType = typeof(BranchTests))]
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
		public void WillGetTheFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.GetFlag(Flag)).MustHaveHappened();
		}

		private void SwitchFlagState() => A.CallTo(() => cpu.GetFlag(Flag)).Returns(!FlagState);
	}
}