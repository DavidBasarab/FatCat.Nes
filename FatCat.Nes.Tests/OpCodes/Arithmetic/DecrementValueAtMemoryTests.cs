using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class DecrementValueAtMemoryTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x5218;

		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1000_1111 // fetched
							};

				yield return new object[]
							{
								0b_1100_0000 // fetched
							};

				yield return new object[]
							{
								0b_0000_0000 // fetched
							};

				yield return new object[]
							{
								0b_1111_1111 // fetched
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
								0b_0000_1111 // fetched
							};

				yield return new object[]
							{
								0b_0100_0000 // fetched
							};

				yield return new object[]
							{
								0b_0111_0000 // fetched
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
								0x02 // fetched
							};

				yield return new object[]
							{
								0xff // fetched
							};

				yield return new object[]
							{
								0x00 // fetched
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
								0x01 // fetched
							};
			}
		}

		protected override string ExpectedName => "DEC";

		public DecrementValueAtMemoryTests()
		{
			opCode = new DecrementValueAtMemory(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheNegativeFlag(byte fetched) => RunFlagSetTest(fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheZeroFlag(byte fetched) => RunFlagSetTest(fetched, CpuFlag.Zero);

		[Fact]
		public void WillTakeNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheNegativeFlag(byte fetched) => RunRemoveFlagTest(fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheZeroFlag(byte fetched) => RunRemoveFlagTest(fetched, CpuFlag.Zero);

		[Fact]
		public void WillWriteTheIncrementDataToMemory()
		{
			opCode.Execute();

			byte expectedWriteData = (FetchedData - 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedWriteData)).MustHaveHappened();
		}

		private void RunFlagSetTest(byte fetched, CpuFlag flag)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
		}

		private void RunRemoveFlagTest(byte fetched, CpuFlag flag)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
		}
	}
}