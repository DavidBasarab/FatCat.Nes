using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Loading;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	public class TransferAccumulatorToXRegisterTests : OpCodeTest
	{
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
								0b_1111_0000, // accumulator
								true          // flag set
							};

				yield return new object[]
							{
								0b_0111_0000, // accumulator
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

		protected override string ExpectedName => "TAX";

		public TransferAccumulatorToXRegisterTests()
		{
			opCode = new TransferAccumulatorToXRegister(cpu, addressMode);

			cpu.XRegister = 0x49;
		}

		[Theory]
		[MemberData(nameof(NegativeFlagData), MemberType = typeof(TransferAccumulatorToXRegisterTests))]
		public void ApplyingTheNegativeFlag(byte accumulator, bool flagSet) => RunApplyFlagTest(accumulator, flagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(TransferAccumulatorToXRegisterTests))]
		public void ApplyingTheZeroFlag(byte accumulator, bool flagSet) => RunApplyFlagTest(accumulator, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillSetTheXRegisterToAccumulatorValue()
		{
			opCode.Execute();

			cpu.XRegister.Should().Be(Accumulator);
		}

		[Fact]
		public void WillTakeZeroCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		private void RunApplyFlagTest(byte accumulator, bool flagSet, CpuFlag flag)
		{
			cpu.Accumulator = accumulator;

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