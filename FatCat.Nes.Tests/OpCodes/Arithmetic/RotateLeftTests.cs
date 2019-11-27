using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class RotateLeftTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0xe1c1;

		public static IEnumerable<object[]> CarryFlagData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};
				
				yield return new object[]
							{
								0b_0000_0000, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};
				
				yield return new object[]
							{
								0b_1111_1111, // fetched
								false,         // carry flag set before fetch
								false         // flag set
							};
				
				yield return new object[]
							{
								0b_0000_0000, // fetched
								false,         // carry flag set before fetch
								false         // flag set
							};
			}
		}
		
		public static IEnumerable<object[]> ZeroFlagData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};
				
				yield return new object[]
							{
								0b_0000_0000, // fetched
								true,         // carry flag set before fetch
								false         // flag set
							};
				
				yield return new object[]
							{
								0b_1111_1111, // fetched
								false,        // carry flag set before fetch
								false         // flag set
							};
				
				yield return new object[]
							{
								0b_0000_0000, // fetched
								false,        // carry flag set before fetch
								true         // flag set
							};
			}
		}

		protected override string ExpectedName => "ROL";

		public RotateLeftTests()
		{
			opCode = new RotateLeft(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void IfAddressModeIsImpliedThenValueIsWrittenToAccumulator()
		{
			A.CallTo(() => addressMode.Name).Returns("Implied");

			opCode.Execute();

			byte valueToWrite = (FetchedData << 1) & 0x00ff;

			cpu.Accumulator.Should().Be(valueToWrite);

			A.CallTo(() => cpu.Write(A<ushort>.Ignored, A<byte>.Ignored)).MustNotHaveHappened();
		}

		[Fact]
		public void IfCarryFlagIsSetWillOrWithCarryFlag()
		{
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(true);

			opCode.Execute();

			byte valueToWrite = ((FetchedData << 1) | 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, valueToWrite)).MustHaveHappened();

			cpu.Accumulator.Should().Be(Accumulator);
		}

		[Theory]
		[MemberData(nameof(CarryFlagData), MemberType = typeof(RotateLeftTests))]
		public void WillApplyTheCarryFlag(byte fetchValue, bool carrySet, bool flagSet) => RunApplyFlagTest(fetchValue, carrySet, flagSet, CpuFlag.CarryBit);
		
		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(RotateLeftTests))]
		public void WillApplyTheZeroFlag(byte fetchValue, bool carrySet, bool flagSet) => RunApplyFlagTest(fetchValue, carrySet, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillWriteTheRightShiftedValue()
		{
			opCode.Execute();

			byte valueToWrite = (FetchedData << 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, valueToWrite)).MustHaveHappened();

			cpu.Accumulator.Should().Be(Accumulator);
		}

		private void RunApplyFlagTest(byte fetchValue, bool carrySet, bool flagSet, CpuFlag flag)
		{
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(carrySet);

			A.CallTo(() => addressMode.Fetch()).Returns(fetchValue);

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