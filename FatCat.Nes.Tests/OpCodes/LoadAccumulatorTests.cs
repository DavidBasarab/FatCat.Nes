using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class LoadAccumulatorTests : OpCodeTest
	{
		public static IEnumerable<object[]> NegativeFlagData
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
								0b_0000_0000, // fetched
								false         // flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // fetched
								true          // flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // fetched
								true          // flag set
							};
			}
		}

		public static IEnumerable<object[]> ZeroFlagData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x00, // fetched
								true  // flag set
							};

				yield return new object[]
							{
								0x01, // fetched
								false // flag set
							};

				yield return new object[]
							{
								0xff, // fetched
								false // flag set
							};
			}
		}

		protected override string ExpectedName => "LDA";

		public LoadAccumulatorTests() => opCode = new LoadAccumulator(cpu, addressMode);

		[Theory]
		[MemberData(nameof(NegativeFlagData), MemberType = typeof(LoadAccumulatorTests))]
		public void ApplyingNegativeFlag(byte fetched, bool flagSet) => RunFlagTest(fetched, flagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(LoadAccumulatorTests))]
		public void ApplyingZeroFlag(byte fetched, bool flagSet) => RunFlagTest(fetched, flagSet, CpuFlag.Zero);

		[Fact]
		public void TheAccumulatorWillBeFetchedValue()
		{
			opCode.Execute();

			cpu.Accumulator.Should().Be(FetchedData);
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillTake1Cycle()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(1);
		}

		private void RunFlagTest(byte fetched, bool flagSet, CpuFlag flag)
		{
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