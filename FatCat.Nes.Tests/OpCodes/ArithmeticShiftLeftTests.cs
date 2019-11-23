using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
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
		public void WillFetchTheDataFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillNotSetTheCarryData(byte fetched)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.CarryBit)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(CpuFlag.CarryBit)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(CarryData), MemberType = typeof(ArithmeticShiftLeftTests))]
		public void WillSetTheCarryFlag(byte fetched)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.CarryBit)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.CarryBit)).MustNotHaveHappened();
		}
	}
}