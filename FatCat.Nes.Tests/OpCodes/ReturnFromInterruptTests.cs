using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ReturnFromInterruptTests : OpCodeTest
	{
		private const int StackPointer = 0xd3;
		private const CpuFlag StatusToReturn = CpuFlag.Negative | CpuFlag.Break | CpuFlag.CarryBit | CpuFlag.Overflow;

		protected override string ExpectedName => "RTI";

		public ReturnFromInterruptTests()
		{
			opCode = new ReturnFromInterrupt(cpu, addressMode);

			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillReadFromTheStackPointer()
		{
			opCode.Execute();

			A.CallTo(() => cpu.Read(0x0100 + StackPointer + 1)).MustHaveHappened();
		}
	}
}