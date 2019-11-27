using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Loading;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class LogicalShiftRightTests : OpCodeTest
	{
		public static IEnumerable<object[]> CarryData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								true          // flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // fetched
								true          // flag set
							};

				yield return new object[]
							{
								0b_1111_1110, // fetched
								false         // flag set
							};
			}
		}

		public static IEnumerable<object[]> NegativeData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // fetched
								false         // flag set
							};

				yield return new object[]
							{
								0b_1111_1110, // fetched
								false         // flag set
							};
			}
		}

		public static IEnumerable<object[]> ZeroData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // fetched
								true          // flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // fetched
								true          // flag set
							};

				yield return new object[]
							{
								0b_1111_1110, // fetched
								false         // flag set
							};
			}
		}

		protected override string ExpectedName => "LSR";

		public LogicalShiftRightTests() => opCode = new LogicalShiftRight(cpu, addressMode);

		[Theory]
		[MemberData(nameof(CarryData), MemberType = typeof(LogicalShiftRightTests))]
		public void WillApplyTheCarryFlag(byte fetched, bool flagSet) => RunApplyFlagTest(fetched, flagSet, CpuFlag.CarryBit);

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(LogicalShiftRightTests))]
		public void WillApplyTheNegativeFlag(byte fetched, bool flagSet) => RunApplyFlagTest(fetched, flagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(LogicalShiftRightTests))]
		public void WillApplyTheZeroFlag(byte fetched, bool flagSet) => RunApplyFlagTest(fetched, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillFetchDataFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		private void RunApplyFlagTest(byte fetched, bool flagSet, CpuFlag carryBit)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			if (flagSet)
			{
				A.CallTo(() => cpu.SetFlag(carryBit)).MustHaveHappened();
				A.CallTo(() => cpu.RemoveFlag(carryBit)).MustNotHaveHappened();
			}
			else
			{
				A.CallTo(() => cpu.RemoveFlag(carryBit)).MustHaveHappened();
				A.CallTo(() => cpu.SetFlag(carryBit)).MustNotHaveHappened();
			}
		}
	}
}