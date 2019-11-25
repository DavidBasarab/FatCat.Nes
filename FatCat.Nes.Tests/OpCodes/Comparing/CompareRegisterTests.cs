using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Comparing
{
	public abstract class CompareRegisterTests : OpCodeTest
	{
		public static IEnumerable<object[]> CarryFlag
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x15, // register
								0x01, // fetched
								true  // carry flag set
							};

				yield return new object[]
							{
								0xf5, // register
								0x00, // fetched
								true  // carry flag set
							};

				yield return new object[]
							{
								0x15, // register
								0xff, // fetched
								false // carry flag set
							};

				yield return new object[]
							{
								0x00, // register
								0x01, // fetched
								false // carry flag set
							};

				yield return new object[]
							{
								0x01, // register
								0x01, // fetched
								true  // carry flag set
							};

				yield return new object[]
							{
								0x00, // register
								0x00, // fetched
								true  // carry flag set
							};
			}
		}

		public static IEnumerable<object[]> NegativeFlag
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // register
								0b_0111_1111, // fetched
								true          // negative flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // register
								0b_0000_0000, // fetched
								true          // negative flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // register
								0b_1111_1111, // fetched
								false         // negative flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // register
								0b_0000_0000, // fetched
								false         // negative flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // register
								0b_0000_0001, // fetched
								true          // negative flag set
							};
			}
		}

		public static IEnumerable<object[]> ZeroFlag
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_0111_1111, // register
								0b_0111_1111, // fetched
								true          // zero flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // register
								0b_1000_0000, // fetched
								true          // zero flag set
							};

				yield return new object[]
							{
								0b_0000_0001, // register
								0b_1111_1111, // fetched
								false         // zero flag set
							};

				yield return new object[]
							{
								0b_1111_1111, // register
								0b_0000_0001, // fetched
								false         // zero flag set
							};
			}
		}

		[Theory]
		[MemberData(nameof(CarryFlag), MemberType = typeof(CompareRegisterTests))]
		public void WillApplyTheCarryFlagCorrectly(byte register, byte fetched, bool carryFlagSet) => RunApplyFlagTest(register, fetched, carryFlagSet, CpuFlag.CarryBit);

		[Theory]
		[MemberData(nameof(NegativeFlag), MemberType = typeof(CompareRegisterTests))]
		public void WillApplyTheNegativeFlagCorrectly(byte register, byte fetched, bool zeroFlagSet) => RunApplyFlagTest(register, fetched, zeroFlagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroFlag), MemberType = typeof(CompareRegisterTests))]
		public void WillApplyTheZeroFlagCorrectly(byte register, byte fetched, bool zeroFlagSet) => RunApplyFlagTest(register, fetched, zeroFlagSet, CpuFlag.Zero);

		[Fact]
		public void WillTake1Cycle()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(1);
		}

		protected abstract void SetRegister(byte registerValue);

		private void RunApplyFlagTest(byte registerValue, byte fetched, bool carryFlagSet, CpuFlag flag)
		{
			SetRegister(registerValue);

			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			VerifyFlag(carryFlagSet, flag);
		}

		private void VerifyFlag(bool flagSet, CpuFlag flag)
		{
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

		[Fact]
		private void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}