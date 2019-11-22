using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AndTests : OpCodeTest
	{
		public static IEnumerable<object[]> NonNegativeData
		{
			get
			{
				yield return new object[]
							{
								2, // accumulator
								3  // fetched
							};

				yield return new object[]
							{
								2, // accumulator
								3  // fetched
							};

				yield return new object[]
							{
								253, // accumulator
								6    // fetched
							};
			}
		}

		public static IEnumerable<object[]> NonZeroData
		{
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
		public void WillFetchTheData()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
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