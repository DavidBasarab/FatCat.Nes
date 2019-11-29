using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public abstract class StoreItemAtAddressTests : OpCodeTest
	{
		protected const int AbsoluteAddress = 0x1915;

		protected abstract byte StoreData { get; }

		[Fact]
		public void Takes0Cycles()
		{
			var cycles = opCode.Execute();

			cycles.Should().Be(0);
		}

		[Fact]
		public void WillWriteAccumulatorToAbsoluteAddress()
		{
			SetDataOnCpu();
			
			cpu.AbsoluteAddress = AbsoluteAddress;

			opCode.Execute();

			A.CallTo(() => cpu.Write(AbsoluteAddress, StoreData)).MustHaveHappened();
		}

		protected abstract void SetDataOnCpu();
	}
}