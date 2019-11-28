using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class RotateRightTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x87e4;

		public static IEnumerable<object[]> CarryFlagData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								true,         // carry flag set before fetch
								true          // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // fetched
								false,        // carry flag set before fetch
								true          // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								false,        // carry flag set before fetch
								false         // flag set
							};
			}
		}

		public static IEnumerable<object[]> NegativeFlagData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								true,         // carry flag set before fetch
								true          // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // fetched
								false,        // carry flag set before fetch
								true          // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								false,        // carry flag set before fetch
								false         // flag set
							};
			}
		}

		public static IEnumerable<object[]> ZeroFlagData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // fetched
								false,        // carry flag set before fetch
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								false,        // carry flag set before fetch
								true          // flag set
							};
			}
		}

		protected override string ExpectedName => "ROR";

		public RotateRightTests()
		{
			opCode = new RotateRight(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void ForImpliedAddressWillSaveToAccumulator()
		{
			A.CallTo(() => addressMode.Name).Returns("Implied");

			opCode.Execute();

			byte expectedValue = (FetchedData >> 1) & 0x00ff;

			cpu.Accumulator.Should().Be(expectedValue);

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedValue)).MustNotHaveHappened();
		}

		[Fact]
		public void ForImpliedAddressWillSaveToAccumulatorWithCarryValue()
		{
			A.CallTo(() => addressMode.Name).Returns("Implied");

			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(true);

			opCode.Execute();

			byte expectedValue = (1 << 7) | ((FetchedData >> 1) & 0x00ff);

			cpu.Accumulator.Should().Be(expectedValue);

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedValue)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(CarryFlagData), MemberType = typeof(RotateRightTests))]
		public void WillApplyCarryFlag(byte fetchValue, bool carrySet, bool flagSet) => RunApplyFlagTest(fetchValue, carrySet, flagSet, CpuFlag.CarryBit);

		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(RotateRightTests))]
		public void WillApplyZeroFlag(byte fetchValue, bool carrySet, bool flagSet) => RunApplyFlagTest(fetchValue, carrySet, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillOrWithCarryValue()
		{
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(true);

			opCode.Execute();

			byte expectedValue = (1 << 7) | ((FetchedData >> 1) & 0x00ff);

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedValue)).MustHaveHappened();
		}

		[Fact]
		public void WillWriteTheShiftedMemoryValue()
		{
			opCode.Execute();

			byte expectedValue = (FetchedData >> 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedValue)).MustHaveHappened();
		}

		private void RunApplyFlagTest(byte fetchValue, bool carrySet, bool flagSet, CpuFlag flag)
		{
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(carrySet);

			A.CallTo(() => addressMode.Fetch()).Returns(fetchValue);

			opCode.Execute();

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
	}
}