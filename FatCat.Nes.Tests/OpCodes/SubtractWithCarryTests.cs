using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class SubtractWithCarryTests : OpCodeTest
	{
		// Test cases found @ http://www.6502.org/tutorials/vflag.html

		public static IEnumerable<object[]> CarryData
		{
			get
			{
				yield return new object[]
							{
								208, // accumulator
								176, // fetched
								true // carry bit set
							};

				yield return new object[]
							{
								208,  // accumulator
								112,  // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								208, // accumulator
								48,  // fetched
								true // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NegativeData
		{
			get
			{
				yield return new object[]
							{
								208, // accumulator
								48,  // fetched
								true // carry bit set
							};

				yield return new object[]
							{
								125, // accumulator
								200, // fetched
								true // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NonCarryData
		{
			get
			{
				yield return new object[]
							{
								80,   // accumulator
								240,  // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								80,  // accumulator
								176, // fetched
								true // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NonNegativeData
		{
			get
			{
				yield return new object[]
							{
								20,   // accumulator
								3,    // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								12,  // accumulator
								3,   // fetched
								true // carry bit set
							};

				yield return new object[]
							{
								110, // accumulator
								6,   // fetched
								true // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NoOverflowData
		{
			get
			{
				yield return new object[]
							{
								0x00, // accumulator
								0x01, // fetched
								true  // carry bit set
							};

				yield return new object[]
							{
								0x50, // accumulator
								0xf0, // fetched
								true  // carry bit set
							};

				yield return new object[]
							{
								0x50, // accumulator
								0x70, // fetched
								false // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> OverflowData
		{
			get
			{
				yield return new object[]
							{
								0x80, // accumulator
								0x01, // fetched
								true  // carry bit set
							};

				yield return new object[]
							{
								0x7f, // accumulator
								0xff, // fetched
								true  // carry bit set
							};

				yield return new object[]
							{
								0xc0, // accumulator
								0x40, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x50, // accumulator
								0xb0, // fetched
								true  // carry bit set
							};

				yield return new object[]
							{
								0xd0, // accumulator
								0x70, // fetched
								false // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> ZeroData
		{
			get
			{
				yield return new object[]
							{
								0x000, // accumulator
								0xff,  // fetched
								false  // carry bit set
							};
			}
		}

		protected override string ExpectedName => "SBC";

		public SubtractWithCarryTests() => opCode = new SubtractWithCarry(cpu, addressMode);

		[Fact]
		public void WillFetchTheData()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillNotSetTheCarryBit(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillNotSetTheNegativeFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Negative)).MustNotHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Negative)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NoOverflowData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillNotSetTheOverflowFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Overflow)).MustNotHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Overflow)).MustHaveHappened();
		}

		[Fact]
		public void WillReadTheCarryFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillRemoveTheZeroFlagForNonOverflowData(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(OverflowData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillSetTheAccumulatorToTheNewTotal(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			var fetchToUse = fetched ^ 0x00ff;

			var expectedTotal = accumulator + fetchToUse + (carry ? 1 : 0);

			var expectedAccumulatorValue = (byte)(expectedTotal & 0x00ff);

			cpu.Accumulator.Should().Be(expectedAccumulatorValue);
		}

		[Theory]
		[MemberData(nameof(CarryData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillSetTheCarryBitIfTotalIsMoreThan255(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillSetTheNegativeFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Negative)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Negative)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(OverflowData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillSetTheOverflowFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Overflow)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Overflow)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(SubtractWithCarryTests))]
		public void WillSetTheZeroFlagIfTheValueIsZero(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustHaveHappened();
		}

		[Fact]
		public void WillTake1Cycle()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(1);
		}

		private void SetUpForExecute(byte accumulator, byte fetched, bool carry)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(carry);
		}
	}
}