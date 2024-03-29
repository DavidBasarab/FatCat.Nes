using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class DecrementXRegisterTests : OpCodeTest
	{
		private const int XRegister = 0x12;

		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1000_1111 // x register
							};

				yield return new object[]
							{
								0b_1100_0000 // x register
							};

				yield return new object[]
							{
								0b_0000_0000 // x register
							};

				yield return new object[]
							{
								0b_1111_1111 // x register
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
								0b_0000_1111 // x register
							};

				yield return new object[]
							{
								0b_0100_0000 // x register
							};

				yield return new object[]
							{
								0b_0111_0000 // x register
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
								0x02 // x register
							};

				yield return new object[]
							{
								0xff // x register
							};

				yield return new object[]
							{
								0x00 // x register
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
								0x01 // x register
							};
			}
		}

		protected override string ExpectedName => "DEX";

		public DecrementXRegisterTests()
		{
			opCode = new DecrementXRegister(cpu, addressMode);

			cpu.XRegister = XRegister;
		}

		[Fact]
		public void WillDecreaseXRegisterValue()
		{
			opCode.Execute();

			cpu.XRegister.Should().Be(XRegister - 1);
		}

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheNegativeFlag(byte xRegister) => RunFlagSetTest(xRegister, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheZeroFlag(byte xRegister) => RunFlagSetTest(xRegister, CpuFlag.Zero);

		[Fact]
		public void WillTakeNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheNegativeFlag(byte xRegister) => RunRemoveFlagTest(xRegister, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheZeroFlag(byte xRegister) => RunRemoveFlagTest(xRegister, CpuFlag.Zero);

		private void RunFlagSetTest(byte xRegister, CpuFlag flag)
		{
			cpu.XRegister = xRegister;

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
		}

		private void RunRemoveFlagTest(byte xRegister, CpuFlag flag)
		{
			cpu.XRegister = xRegister;

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
		}
	}
}