using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class DecrementYRegisterTests : OpCodeTest
	{
		private const int YRegister = 0x12;

		public static IEnumerable<object[]> NegativeData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0b_1000_1111 // y register
							};

				yield return new object[]
							{
								0b_1100_0000 // y register
							};

				yield return new object[]
							{
								0b_0000_0000 // y register
							};

				yield return new object[]
							{
								0b_1111_1111 // y register
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
								0b_0000_1111 // y register
							};

				yield return new object[]
							{
								0b_0100_0000 // y register
							};

				yield return new object[]
							{
								0b_0111_0000 // y register
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
								0x02 // y register
							};

				yield return new object[]
							{
								0xff // y register
							};

				yield return new object[]
							{
								0x00 // y register
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
								0x01 // y register
							};
			}
		}

		protected override string ExpectedName => "DEY";

		public DecrementYRegisterTests()
		{
			opCode = new DecrementYRegister(cpu, addressMode);

			cpu.YRegister = YRegister;
		}

		[Fact]
		public void WillDecreaseYRegisterValue()
		{
			opCode.Execute();

			cpu.YRegister.Should().Be(YRegister - 1);
		}

		[Theory]
		[MemberData(nameof(NegativeData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheNegativeFlag(byte yRegister) => RunFlagSetTest(yRegister, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheZeroFlag(byte yRegister) => RunFlagSetTest(yRegister, CpuFlag.Zero);

		[Fact]
		public void WillTakeNoCycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Theory]
		[MemberData(nameof(NonNegativeData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheNegativeFlag(byte yRegister) => RunRemoveFlagTest(yRegister, CpuFlag.Negative);

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheZeroFlag(byte yRegister) => RunRemoveFlagTest(yRegister, CpuFlag.Zero);

		private void RunFlagSetTest(byte yRegister, CpuFlag flag)
		{
			cpu.YRegister = yRegister;

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(flag)).MustNotHaveHappened();
		}

		private void RunRemoveFlagTest(byte yRegister, CpuFlag flag)
		{
			cpu.YRegister = yRegister;

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(flag)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(flag)).MustNotHaveHappened();
		}
	}
}