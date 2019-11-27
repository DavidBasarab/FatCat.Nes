using FakeItEasy;
using FatCat.Nes.OpCodes.IO;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PushStatusRegisterToStackTests : OpCodeTest
	{
		private const int StackPointer = 0x52;
		private const CpuFlag StartingStatusRegister = CpuFlag.Zero | CpuFlag.CarryBit;

		protected override string ExpectedName => "PHP";

		public PushStatusRegisterToStackTests()
		{
			opCode = new PushStatusRegisterToStack(cpu, addressMode);

			cpu.StatusRegister = StartingStatusRegister;
			cpu.StackPointer = StackPointer;
		}

		[Fact]
		public void WillWriteStatusRegisterToStack()
		{
			opCode.Execute();

			var expectedStack = StartingStatusRegister | CpuFlag.Break | CpuFlag.Unused;

			A.CallTo(() => cpu.Write(0x0100 + StackPointer, (byte)expectedStack)).MustHaveHappened();
		}
	}
}