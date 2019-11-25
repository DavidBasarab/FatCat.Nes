using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class CompareAccumulatorTests : OpCodeTest
	{
		public static IEnumerable<object[]> CarryFlag
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x15, // accumulator
								0x01, // fetched
								true  // carry flag set
							};

				yield return new object[]
							{
								0xf5, // accumulator
								0x00, // fetched
								true  // carry flag set
							};

				yield return new object[]
							{
								0x15, // accumulator
								0xff, // fetched
								false // carry flag set
							};

				yield return new object[]
							{
								0x00, // accumulator
								0x01, // fetched
								false // carry flag set
							};

				yield return new object[]
							{
								0x01, // accumulator
								0x01, // fetched
								true  // carry flag set
							};

				yield return new object[]
							{
								0x00, // accumulator
								0x00, // fetched
								true  // carry flag set
							};
			}
		}

		public static IEnumerable<object[]> NegativeFlag
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_0111_1111, // fetched
								true          // negative flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // accumulator
								0b_0000_0000, // fetched
								true          // negative flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // accumulator
								0b_1111_1111, // fetched
								false         // negative flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator
								0b_0000_0000, // fetched
								false         // negative flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_0000_0001, // fetched
								true          // negative flag set
							};
			}
		}

		public static IEnumerable<object[]> ZeroFlag
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_0111_1111, // accumulator
								0b_0111_1111, // fetched
								true          // zero flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // accumulator
								0b_1000_0000, // fetched
								true          // zero flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // accumulator
								0b_1111_1111, // fetched
								false         // zero flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_0000_0001, // fetched
								false         // zero flag set
							};
			}
		}

		protected override string ExpectedName => "CMP";

		public CompareAccumulatorTests() => opCode = new CompareAccumulator(cpu, addressMode);

		[Theory]
		[MemberData(nameof(CarryFlag), MemberType = typeof(CompareAccumulatorTests))]
		public void WillApplyTheCarryFlagCorrectly(byte accumulator, byte fetched, bool carryFlagSet) => RunApplyFlagTest(accumulator, fetched, carryFlagSet, CpuFlag.CarryBit);

		[Theory]
		[MemberData(nameof(NegativeFlag), MemberType = typeof(CompareAccumulatorTests))]
		public void WillApplyTheNegativeFlagCorrectly(byte accumulator, byte fetched, bool zeroFlagSet) => RunApplyFlagTest(accumulator, fetched, zeroFlagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroFlag), MemberType = typeof(CompareAccumulatorTests))]
		public void WillApplyTheZeroFlagCorrectly(byte accumulator, byte fetched, bool zeroFlagSet) => RunApplyFlagTest(accumulator, fetched, zeroFlagSet, CpuFlag.Zero);

		[Fact]
		public void WillTake1Cycle()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(1);
		}

		private void RunApplyFlagTest(byte accumulator, byte fetched, bool carryFlagSet, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			VerifyFlag(carryFlagSet, flag);
		}

		private void VerifyFlag(bool flagSet, CpuFlag flag)
		{
			if (flagSet)
			{
				A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
				A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
			}
			else
			{
				A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
				A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
			}
		}

		[Fact]
		private void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}