using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BitwiseLogicOrTests : OpCodeTest
	{
		public static IEnumerable<object[]> ZeroData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_1111_1111, // fetched
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator
								0b_0000_0000, // fetched
								true          // flag set
							};

				yield return new object[]
							{
								0b_1010_1010, // accumulator
								0b_0111_0101, // fetched
								false         // flag set
							};
			}
		}

		protected override string ExpectedName => "ORA";

		public BitwiseLogicOrTests() => opCode = new BitwiseLogicOr(cpu, addressMode);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(BitwiseLogicOrTests))]
		public void WillApplyZeroFlag(byte accumulator, byte fetched, bool flagSet) => RunApplyFlagTest(accumulator, fetched, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillOrTheAccumulatorWithMemoryData()
		{
			opCode.Execute();

			byte expectedValue = Accumulator | FetchedData;

			cpu.Accumulator.Should().Be(expectedValue);
		}

		private void RunApplyFlagTest(byte accumulator, byte fetched, bool flagSet, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

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