using FakeItEasy;
using FatCat.Nes.OpCodes.IO;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PushAccumulatorTests : OpCodeTest
	{
		private const int StackPointer = 0xa4;

		protected override string ExpectedName => "PHA";

		public PushAccumulatorTests()
		{
			opCode = new PushAccumulator(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void StackPointerWillBeReducedBy1()
		{
			opCode.Execute();

			cpu.StackPointer.Should().Be(StackPointer - 1);
		}

		[Fact]
		public void WillTake0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WillWriteToTheStackTheValueOfTheAccumulator()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Write(0x0100 + StackPointer, Accumulator)).MustHaveHappened();
		}
	}
}