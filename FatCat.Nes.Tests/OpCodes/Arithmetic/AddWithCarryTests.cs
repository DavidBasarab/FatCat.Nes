using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class AddWithCarryTests : OpCodeTest
	{
		// Test cases found @ http://www.6502.org/tutorials/vflag.html

		public static IEnumerable<object[]> CarryData
		{
			get
			{
				yield return new object[]
							{
								0xb2, // accumulator
								0x62, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x60, // accumulator
								0x9f, // fetched
								true  // carry bit set
							};

				yield return new object[]
							{
								0x01, // accumulator
								0xff, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x80, // accumulator
								0xff, // fetched
								false // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NegativeData
		{
			get
			{
				yield return new object[]
							{
								2,    // accumulator
								253,  // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								125, // accumulator
								2,   // fetched
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
								0x32, // accumulator
								0x25, // fetched
								false // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NonNegativeData
		{
			get
			{
				yield return new object[]
							{
								2,    // accumulator
								3,    // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								2,   // accumulator
								3,   // fetched
								true // carry bit set
							};

				yield return new object[]
							{
								253, // accumulator
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
								0x01, // accumulator
								0x01, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x01, // accumulator
								0xff, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x00, // accumulator
								0x01, // fetched
								true  // carry bit set
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
								0xff, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x7f, // accumulator
								0x01, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x3f, // accumulator
								0x40, // fetched
								true  // carry bit set
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
								0x000, // fetched
								false  // carry bit set
							};
			}
		}

		protected override string ExpectedName => "ADC";

		public AddWithCarryTests() => opCode = new AddWithCarry(cpu, addressMode);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(AddWithCarryTests))]
		public void WillNotSetTheNegativeFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Negative)).MustNotHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Negative)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NoOverflowData), MemberType = typeof(AddWithCarryTests))]
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
		[MemberData(nameof(NonCarryData), MemberType = typeof(AddWithCarryTests))]
		public void WillRemoveTheZeroFlagForNonOverflowData(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(OverflowData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheAccumulatorToTheNewTotal(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			var expectedTotal = accumulator + fetched + (carry ? 1 : 0);

			var expectedAccumulatorValue = (byte)(expectedTotal & 0x00ff);

			cpu.Accumulator.Should().Be(expectedAccumulatorValue);
		}

		[Theory]
		[MemberData(nameof(CarryData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheCarryBitIfTotalIsMoreThan255(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheCarryBitToFalseInNonOverflow(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheNegativeFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Negative)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Negative)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(OverflowData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheOverflowFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExecute(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Overflow)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Overflow)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(AddWithCarryTests))]
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