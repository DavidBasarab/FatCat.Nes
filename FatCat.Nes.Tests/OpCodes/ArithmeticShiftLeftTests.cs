using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ArithmeticShiftLeftTests : OpCodeTest
	{
		protected override string ExpectedName => "ASL";

		public ArithmeticShiftLeftTests() => opCode = new ArithmeticShiftLeft(cpu, addressMode);

		[Fact]
		public void WillFetchTheDataFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}