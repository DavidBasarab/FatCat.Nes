using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BitOpCodeTests : OpCodeTest
	{
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
		[MemberData(nameof(NonZeroData), MemberType = typeof(BitOpCodeTests))]
		public void WillRemoveTheZeroFlag(byte accumulator, byte fetched)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(BitOpCodeTests))]
		public void WillSetTheZeroFlag(byte accumulator, byte fetched)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustNotHaveHappened();
		}

		[Fact]
		public void WillTakeNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}
	}
}