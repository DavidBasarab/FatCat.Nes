using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuFlagsTest : CpuBaseTests
	{
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
	}
}