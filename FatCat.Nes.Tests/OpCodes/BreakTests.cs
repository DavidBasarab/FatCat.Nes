using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class BreakTests : OpCodeTest
	{
		protected override string ExpectedName => "BRK";

		public BreakTests() => opCode = new Break(cpu, addressMode);

		[Fact]
		public void WillSetTheInterruptFlag()
		{
			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.DisableInterrupts)).MustHaveHappened();
		}
	}
}