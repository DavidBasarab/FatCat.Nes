using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.IO;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PopAccumulatorOffStackTests : OpCodeTest
	{
		private const byte AccumulatorValueRead = 0x57;
		private const int StackPointer = 0xe8;

		public static IEnumerable<object[]> NegativeFlagData
		{
			get
			{
				yield return new object[]
							{
								0b_1111_1111, // accumulator 
								true          // flag set
							};

				yield return new object[]
							{
								0b_1000_0000, // accumulator 
								true          // flag set
							};

				yield return new object[]
							{
								0b_0111_1111, // accumulator 
								false         // flag set
							};

				yield return new object[]
							{
								0b_0000_0000, // accumulator 
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
								0x00, // accumulator 
								true  // flag set
							};

				yield return new object[]
							{
								0xff, // accumulator 
								false // flag set
							};

				yield return new object[]
							{
								0x01, // accumulator 
								false // flag set
							};
			}
		}

		protected override string ExpectedName => "PLA";

		public PopAccumulatorOffStackTests()
		{
			opCode = new PopAccumulatorOffStack(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void StackPointerWillBe1MoreAfterRead()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(StackPointer + 1);
		}

		[Theory]
		[MemberData(nameof(NegativeFlagData), MemberType = typeof(PopAccumulatorOffStackTests))]
		public void WillApplyTheNegativeFlag(byte readValue, bool flagSet) => RunApplyFlagTest(readValue, flagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(PopAccumulatorOffStackTests))]
		public void WillApplyTheZeroFlag(byte readValue, bool flagSet) => RunApplyFlagTest(readValue, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillReadFromTheNextStackPointer()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).MustHaveHappened();
		}

		[Fact]
		public void WillSetAccumulatorToReadValue()
		{
			SetUpReadFromStack(AccumulatorValueRead);

			opCode.Execute();

			cpu.Accumulator.Should().Be(AccumulatorValueRead);
		}

		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		private void RunApplyFlagTest(byte readValue, bool flagSet, CpuFlag flag)
		{
			SetUpReadFromStack(readValue);

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

		private void SetUpReadFromStack(byte accumulatorValue) { A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).Returns(accumulatorValue); }
	}
}