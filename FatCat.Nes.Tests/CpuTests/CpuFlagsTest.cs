using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuFlagsTest : CpuBaseTests
	{
		[Theory]
		[InlineData(CpuFlag.Break, CpuFlag.Overflow, CpuFlag.Negative)]
		[InlineData(CpuFlag.CarryBit, CpuFlag.Overflow, CpuFlag.DecimalMode)]
		[InlineData(CpuFlag.Negative, CpuFlag.Zero, CpuFlag.Overflow)]
		public void CanRemoveAFlag(CpuFlag firstFlag, CpuFlag secondFlag, CpuFlag thirdFlag)
		{
			cpu.SetFlag(firstFlag);
			cpu.SetFlag(secondFlag);
			cpu.SetFlag(thirdFlag);

			TestRemoveFlag(firstFlag);

			cpu.GetFlag(secondFlag).Should().BeTrue();
			cpu.GetFlag(thirdFlag).Should().BeTrue();

			TestRemoveFlag(secondFlag);

			cpu.GetFlag(thirdFlag).Should().BeTrue();
		}

		[Theory]
		[InlineData(CpuFlag.Break)]
		[InlineData(CpuFlag.Overflow)]
		[InlineData(CpuFlag.CarryBit)]
		public void CanSetAFlag(CpuFlag flag)
		{
			cpu.SetFlag(flag);

			cpu.GetFlag(flag).Should().BeTrue();
		}

		[Theory]
		[InlineData(CpuFlag.Break, CpuFlag.Overflow, CpuFlag.Negative)]
		[InlineData(CpuFlag.CarryBit, CpuFlag.Overflow, CpuFlag.DecimalMode)]
		[InlineData(CpuFlag.Negative, CpuFlag.Zero, CpuFlag.Overflow)]
		public void CanSetMultipleFlags(CpuFlag firstFlag, CpuFlag secondFlag, CpuFlag flagNotSet)
		{
			cpu.SetFlag(firstFlag);
			cpu.SetFlag(secondFlag);

			cpu.GetFlag(firstFlag).Should().BeTrue();
			cpu.GetFlag(secondFlag).Should().BeTrue();
			cpu.GetFlag(flagNotSet).Should().BeFalse();
		}

		[Theory]
		[InlineData(CpuFlag.Break)]
		[InlineData(CpuFlag.Overflow)]
		[InlineData(CpuFlag.CarryBit)]
		public void CanSetTheSameFlagMoreThanOnce(CpuFlag flag)
		{
			cpu.SetFlag(flag);
			cpu.SetFlag(flag);
			cpu.SetFlag(flag);

			cpu.GetFlag(flag).Should().BeTrue();
		}

		private void TestRemoveFlag(CpuFlag flag)
		{
			cpu.RemoveFlag(flag);

			cpu.GetFlag(flag).Should().BeFalse();
		}
	}
}