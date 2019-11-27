using FakeItEasy;
using FatCat.Nes.OpCodes.IO;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PopStatusOffStackTests : OpCodeTest
	{
		private const CpuFlag ReturnedFlag = CpuFlag.Overflow | CpuFlag.Negative;
		private const int StackPointer = 0xa8;

		protected override string ExpectedName => "PLP";

		public PopStatusOffStackTests()
		{
			opCode = new PopStatusOffStack(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void ThisWillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WillIncreaseTheStackPointerBy1()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(StackPointer + 1);
		}

		[Fact]
		public void WillReadFromTheStack()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).MustHaveHappened();
		}

		[Fact]
		public void WillRemoveTheUsedFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Unused)).MustHaveHappened();
		}

		[Fact]
		public void WillSetTheReturnStatusToTheCpu()
		{
			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).Returns((byte)ReturnedFlag);

			opCode.Execute();

			cpu.StatusRegister.Should().Be(ReturnedFlag);
		}
	}
}