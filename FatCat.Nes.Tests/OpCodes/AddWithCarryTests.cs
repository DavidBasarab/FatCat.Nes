using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AddWithCarryTests
	{
		private const byte FetchedData = 0x13;

		public static IEnumerable<object[]> CarryData
		{
			get
			{
				yield return new object[]
							{
								0xb2, // accumulator
								0x62, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x60, // accumulator
								0x9f, // fetched
								true  // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NonCarryData
		{
			get
			{
				yield return new object[]
							{
								0x32, // accumulator
								0x25, // fetched
								false // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> NoOverflowData
		{
			get
			{
				yield return new object[]
							{
								0x01, // accumulator
								0x01, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x01, // accumulator
								0xff, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x00, // accumulator
								0x01, // fetched
								true  // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> OverflowData
		{
			get
			{
				yield return new object[]
							{
								0x80, // accumulator
								0xff, // fetched
								false // carry bit set
							};

				yield return new object[]
							{
								0x7f, // accumulator
								0x01, // fetched
								false // carry bit set
							};
			}
		}

		public static IEnumerable<object[]> ZeroData
		{
			get
			{
				yield return new object[]
							{
								0x000, // accumulator
								0x000, // fetched
								false  // carry bit set
							};
			}
		}

		private readonly IAddressMode addressMode;

		private readonly ICpu cpu;

		private readonly AddWithCarry opCode;

		public AddWithCarryTests()
		{
			cpu = A.Fake<ICpu>();
			addressMode = A.Fake<IAddressMode>();

			A.CallTo(() => addressMode.Fetch()).Returns(FetchedData);

			opCode = new AddWithCarry(cpu, addressMode);
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NoOverflowData), MemberType = typeof(AddWithCarryTests))]
		public void WillNotSetTheOverflowFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExectue(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Overflow)).MustNotHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Overflow)).MustHaveHappened();
		}

		[Fact]
		public void WillReadTheCarryFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(AddWithCarryTests))]
		public void WillRemoveTheZeroFlagForNonOverflowData(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExectue(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(CarryData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheCarryBitIfTotalIsMoreThan255(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExectue(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonCarryData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheCarryBitToFalseInNonOverflow(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExectue(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.CarryBit)).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(OverflowData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheOverflowFlag(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExectue(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Overflow)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Overflow)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(AddWithCarryTests))]
		public void WillSetTheZeroFlagIfTheValueIsZero(byte accumulator, byte fetched, bool carry)
		{
			SetUpForExectue(accumulator, fetched, carry);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustHaveHappened();
		}

		private void SetUpForExectue(byte accumulator, byte fetched, bool carry)
		{
			cpu.Accumulator = accumulator;

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(carry);
		}
	}
}