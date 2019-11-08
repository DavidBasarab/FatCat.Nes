using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ZeroPageXOffsetTests : AddressModeTests
	{
		protected override string ExpectedName => "ZeroPage,X";

		public ZeroPageXOffsetTests() => addressMode = new ZeroPageXOffset(cpu);

		[Fact]
		public void ItWillReadFromTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}
	}
}