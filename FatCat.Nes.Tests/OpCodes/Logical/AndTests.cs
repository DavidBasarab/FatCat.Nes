using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Logical;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Logical
{
	public class AndTests : OpCodeTest
	{
		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1101_1001, // accumulator
								0b_1001_1111  // fetched
							};

				yield return new object[]
							{
								0b_1000_0000, // accumulator
								0b_1111_1111  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_1111_1111  // fetched
							};
			}
		}

		public static IEnumerable<object[]> NonNegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_0101_1001, // accumulator
								0b_0001_1111  // fetched
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator
								0b_0111_1111  // fetched
							};

				yield return new object[]
							{
								0b_0111_1111, // accumulator
								0b_0111_1111  // fetched
							};
			}
		}

		public static IEnumerable<object[]> NonZeroData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x1f, // accumulator
								0xe1  // fetched
							};

				yield return new object[]
							{
								0xa1, // accumulator
								0x01  // fetched
							};

				yield return new object[]
							{
								0x10, // accumulator
								0x10  // fetched
							};
			}
		}

		public static IEnumerable<object[]> ZeroData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x00, // accumulator
								0x00  // fetched
							};

				yield return new object[]
							{
								0xa0, // accumulator
								0x01  // fetched
							};
			}
		}

		protected override string ExpectedName => "AND";

		public AndTests() => opCode = new And(cpu, addressMode);

		[Fact]
		public void AndWillTake1Cycle()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(1);
		}

		[Fact]
		public void WillFetchTheData()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(AndTests))]
		public void WillRemoveNegativeFlagIfDataIsNotNegative(byte accumulator, byte fetched)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Negative)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(CpuFlag.Negative)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(AndTests))]
		public void WillRemoveZeroFlagIfDataIsNotZero(byte accumulator, byte fetched)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustNotHaveHappened();
		}

		[Fact]
		public void WillSetTheAccumulatorToBitWiseAndWithFetchedValue()
		{
			opCode.Execute();

			byte expectedValue = Accumulator & FetchedData;

			cpu.Accumulator.Should().Be(expectedValue);
		}

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(AndTests))]
		public void WillSetTheNegativeFlagIfDataIsNegative(byte accumulator, byte fetched)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Negative)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Negative)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(AndTests))]
		public void WillSetTheZeroBitIfDataIsZero(byte accumulator, byte fetched)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustNotHaveHappened();
		}
	}
}