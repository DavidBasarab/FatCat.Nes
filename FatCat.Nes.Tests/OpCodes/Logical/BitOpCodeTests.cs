using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Logical;
using FatCat.Nes.Tests.OpCodes.Repository;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Logical
{
	public class BitOpCodeTests : OpCodeTest
	{
		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1000_1111, // accumulator
								0b_1111_0001  // fetched
							};

				yield return new object[]
							{
								0b_1110_1111, // accumulator
								0b_1111_0001  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_1000_0000  // fetched
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
								0b_0000_1111, // accumulator
								0b_0111_0001  // fetched
							};

				yield return new object[]
							{
								0x0f, // accumulator
								0x03  // fetched
							};

				yield return new object[]
							{
								0b_0110_1111, // accumulator
								0b_0111_0001  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
								0b_0100_0000  // fetched
							};
			}
		}

		public static IEnumerable<object[]> NonOverflowData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_0000_1111, // accumulator
								0b_1011_0001  // fetched
							};

				yield return new object[]
							{
								0b_0110_1111, // accumulator
								0b_1000_0000  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
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
								0b_0000_1111, // accumulator
								0b_1111_0001  // fetched
							};

				yield return new object[]
							{
								0x0f, // accumulator
								0x03  // fetched
							};

				yield return new object[]
							{
								0x03, // accumulator
								0x11  // fetched
							};
			}
		}

		public static IEnumerable<object[]> OverflowData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1000_1111, // accumulator
								0b_1111_0001  // fetched
							};

				yield return new object[]
							{
								0b_1110_1111, // accumulator
								0b_0100_0000  // fetched
							};

				yield return new object[]
							{
								0b_1111_1111, // accumulator
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
								0x02, // accumulator
								0x00  // fetched
							};

				yield return new object[]
							{
								0x00, // accumulator
								0x03  // fetched
							};

				yield return new object[]
							{
								0x03, // accumulator
								0x00  // fetched
							};
			}
		}

		protected override string ExpectedName => "BIT";

		public BitOpCodeTests() => opCode = new BitOpCode(cpu, addressMode);

		[Fact]
		public void WillFetchTheData()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(BitOpCodeTests))]
		public void WillRemoveTheNegativeFlag(byte accumulator, byte fetched) => RunRemoveFlagTest(accumulator, fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(NonOverflowData), MemberType = typeof(BitOpCodeTests))]
		public void WillRemoveTheOverflowFlag(byte accumulator, byte fetched) => RunRemoveFlagTest(accumulator, fetched, CpuFlag.Overflow);

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(BitOpCodeTests))]
		public void WillRemoveTheZeroFlag(byte accumulator, byte fetched) => RunRemoveFlagTest(accumulator, fetched, CpuFlag.Zero);

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(BitOpCodeTests))]
		public void WillSetTheNegativeFlag(byte accumulator, byte fetched) => RunSetFlagTest(accumulator, fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(OverflowData), MemberType = typeof(BitOpCodeTests))]
		public void WillSetTheOverflowFlag(byte accumulator, byte fetched) => RunSetFlagTest(accumulator, fetched, CpuFlag.Overflow);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(BitOpCodeTests))]
		public void WillSetTheZeroFlag(byte accumulator, byte fetched) => RunSetFlagTest(accumulator, fetched, CpuFlag.Zero);

		[Fact]
		public void WillTakeNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		private void RunRemoveFlagTest(byte accumulator, byte fetched, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
		}

		private void RunSetFlagTest(byte accumulator, byte fetched, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
		}
	}
}