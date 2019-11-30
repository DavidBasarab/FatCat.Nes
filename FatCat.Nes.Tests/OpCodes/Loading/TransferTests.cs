using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	public abstract class TransferTests : OpCodeTest
	{
		public static IEnumerable<object[]> NegativeFlagData
		{
			[UsedImplicitly]
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
			[UsedImplicitly]
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

		protected abstract byte CpuTransferFromItem { get; set; }

		protected abstract byte ExpectedValue { get; }

		private byte CpuTransferItem => cpu.XRegister;

		[Theory]
		[MemberData(nameof(NegativeFlagData), MemberType = typeof(TransferTests))]
		public void ApplyingTheNegativeFlag(byte transferFromValue, bool flagSet) => RunApplyFlagTest(transferFromValue, flagSet, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(TransferTests))]
		public void ApplyingTheZeroFlag(byte transferFromValue, bool flagSet) => RunApplyFlagTest(transferFromValue, flagSet, CpuFlag.Zero);

		[Fact]
		public void WillSetTheXRegisterToAccumulatorValue()
		{
			SetUpCpuInitialValues();

			opCode.Execute();

			CpuTransferItem.Should().Be(ExpectedValue);
		}

		[Fact]
		public void WillTakeZeroCycles()
		{
			SetUpCpuInitialValues();

			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		protected abstract void SetUpCpuInitialValues();

		private void RunApplyFlagTest(byte accumulator, bool flagSet, CpuFlag flag)
		{
			CpuTransferFromItem = accumulator;

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