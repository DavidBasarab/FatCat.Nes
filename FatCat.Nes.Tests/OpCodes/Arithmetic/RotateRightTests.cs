using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class RotateRightTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x87e4;

		protected override string ExpectedName => "ROR";

		public RotateRightTests()
		{
			opCode = new RotateRight(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}