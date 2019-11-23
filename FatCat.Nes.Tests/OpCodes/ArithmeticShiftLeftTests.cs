using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ArithmeticShiftLeftTests : OpCodeTest
	{
		public static IEnumerable<object[]> CarryData
		{
			get
			{
				yield return new object[]
							{
								0b_1001_1111 // fetched
							};

				yield return new object[]
							{
								0b_1111_1111 // fetched
							};

				yield return new object[]
							{
								0b_1000_0000 // fetched
							};
			}
		}

		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1101_1111 // fetched
							};

				yield return new object[]
							{
								0b_0111_1111 // fetched
							};

				yield return new object[]
							{
								0b_1111_1111 // fetched
							};
			}
		}

		public static IEnumerable<object[]> NonCarryData
		{
			get
			{
				yield return new object[]
							{
								0b_0001_1111 // fetched
							};

				yield return new object[]
							{
								0b_0111_1111 // fetched
							};

				yield return new object[]
							{
								0b_0000_0000 // fetched
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
								0b_0011_1111 // fetched
							};

				yield return new object[]
							{
								0b_1011_1111 // fetched
							};

				yield return new object[]
							{
								0b_0000_0000 // fetched
							};

				yield return new object[]
							{
								0b_0000_0001 // fetched
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
								0b_0011_1001 // fetched
							};

				yield return new object[]
							{
								0b_1001_1111 // fetched
							};

				yield return new object[]
							{
								0b_0010_0010 // fetched
							};

				yield return new object[]
							{
								0b_0000_0001 // fetched
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
								0b_0000_0000 // fetched
							};

				yield return new object[]
							{
								0b_1000_0000 // fetched
							};
			}
		}

		protected override string ExpectedName => "ASL";

		public ArithmeticShiftLeftTests() => opCode = new ArithmeticShiftLeft(cpu, addressMode);
		
		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void IfAddressModeIsImpliedTheShiftedValueIsWrittenToAccumulator()
		{
			A.CallTo(() => addressMode.Name).Returns("Implied");

			opCode.Execute();

			byte expectedValue = (FetchedData << 1) & 0x00ff;

			cpu.Accumulator.Should().Be(expectedValue);
			
			A.CallTo(() => cpu.Write(cpu.AbsoluteAddress, expectedValue)).MustNotHaveHappened();
		}

		[Fact]
		public void WillFetchTheDataFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillNotSetTheCarryFlag(byte fetched) => RunFlagNotSetTest(fetched, CpuFlag.CarryBit);

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillNotSetTheNegativeFlag(byte fetched) => RunFlagNotSetTest(fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillNotSetTheZeroFlag(byte fetched) => RunFlagNotSetTest(fetched, CpuFlag.Zero);

		[Theory]
		[MemberData(nameof(CarryData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillSetTheCarryFlag(byte fetched) => RunFlagSetTest(fetched, CpuFlag.CarryBit);

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillSetTheNegativeFlag(byte fetched) => RunFlagSetTest(fetched, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillSetTheZeroFlag(byte fetched) => RunFlagSetTest(fetched, CpuFlag.Zero);

		[Fact]
		public void WillWriteTheShiftedValueTheBus()
		{
			opCode.Execute();

			byte expectedValue = (FetchedData << 1) & 0x00ff;

			A.CallTo(() => cpu.Write(cpu.AbsoluteAddress, expectedValue)).MustHaveHappened();

			cpu.Accumulator.Should().Be(Accumulator);
		}

		private void RunFlagNotSetTest(byte fetched, CpuFlag flag)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
		}

		private void RunFlagSetTest(byte fetched, CpuFlag flag)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
		}
	}
}