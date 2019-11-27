using FakeItEasy;
using FatCat.Nes.OpCodes.IO;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PopStatusOffStackTests : OpCodeTest
	{
		private const int StackPointer = 0xa8;

		private const CpuFlag ReturnedFlag = CpuFlag.Overflow | CpuFlag.Negative;

		protected override string ExpectedName => "PLP";

		public PopStatusOffStackTests()
		{
			opCode = new PopStatusOffStack(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillReadFromTheStack()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).MustHaveHappened();
		}
	}
}