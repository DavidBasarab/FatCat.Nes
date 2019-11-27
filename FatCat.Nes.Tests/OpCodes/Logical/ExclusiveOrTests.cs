using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Logical;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Logical
{
	public class ExclusiveOrTests : OpCodeTest
	{
		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1000_1111, // accumulator,
								0b_0000_1111  // fetched
							};

				yield return new object[]
							{
								0b_0000_1111, // accumulator,
								0b_1000_1111  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator,
								0b_0000_0000  // fetched
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator,
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
								0b_1000_1111, // accumulator,
								0b_1000_1111  // fetched
							};

				yield return new object[]
							{
								0b_0000_1111, // accumulator,
								0b_0000_1111  // fetched
							};

				yield return new object[]
							{
								0b_0000_1111, // accumulator,
								0b_0100_1111  // fetched
							};

				yield return new object[]
							{
								0b_0100_1111, // accumulator,
								0b_0000_1111  // fetched
							};

				yield return new object[]
							{
								0b_1000_0000, // accumulator,
								0b_1000_0000  // fetched
							};

				yield return new object[]
							{
								0b_1000_0010, // accumulator,
								0b_1000_0100  // fetched
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator,
								0b_0000_0000  // fetched
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
								0b_1000_1111, // accumulator,
								0b_1000_1101  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator,
								0b_0000_0000  // fetched
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator,
								0b_1111_1111  // fetched
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
								0b_1000_1111, // accumulator,
								0b_1000_1111  // fetched
							};

				yield return new object[]
							{
								0b_0111_0000, // accumulator,
								0b_0111_0000  // fetched
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator,
								0b_0000_0000  // fetched
							};

				yield return new object[]
							{
								0b_0000_1000, // accumulator,
								0b_0000_1000  // fetched
							};
			}
		}

		protected override string ExpectedName => "EOR";

		public ExclusiveOrTests() => opCode = new ExclusiveOr(cpu, addressMode);

		[Fact]
		public void WillDoXorOnAccumulator()
		{
			opCode.Execute();

			byte expectedAccumulator = Accumulator ^ FetchedData;

			cpu.Accumulator.Should().Be(expectedAccumulator);
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(ExclusiveOrTests))]
		public void WillRemoveTheNegativeFlag(byte accumulator, byte fetched) => RunFlagRemoveTest(accumulator, fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(ExclusiveOrTests))]
		public void WillRemoveTheZeroFlag(byte accumulator, byte fetched) => RunFlagRemoveTest(accumulator, fetched, CpuFlag.Zero);

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(ExclusiveOrTests))]
		public void WillSetTheNegativeFlag(byte accumulator, byte fetched) => RunFlagSetTest(accumulator, fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(ExclusiveOrTests))]
		public void WillSetTheZeroFlag(byte accumulator, byte fetched) => RunFlagSetTest(accumulator, fetched, CpuFlag.Zero);

		[Fact]
		public void WillTake1Cycle()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(1);
		}

		private void RunFlagRemoveTest(byte accumulator, byte fetched, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
		}

		private void RunFlagSetTest(byte accumulator, byte fetched, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
		}
	}
}