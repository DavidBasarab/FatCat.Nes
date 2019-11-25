using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class DecrementValueAtMemoryTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x5218;

		protected override string ExpectedName => "DEC";

		public DecrementValueAtMemoryTests()
		{
			opCode = new DecrementValueAtMemory(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillWriteTheIncrementDataToMemory()
		{
			opCode.Execute();

			byte expectedWriteData = (FetchedData - 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedWriteData)).MustHaveHappened();
		}
	}
}