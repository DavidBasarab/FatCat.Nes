using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ZeroPageModeTests : AddressModeTests
	{
		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "ZeroPage";

		public ZeroPageModeTests() => addressMode = new ZeroPageMode(cpu);

		[Fact]
		public void WillIncrementTheProgramCounter()
		{
			addressMode.Run();

			cpu.ProgramCounter.Should().Be(ProgramCounter + 1);
		}

		[Fact]
		public void WillReadFromCpuAtTheProgramCounter()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillSetAbsoluteAddressToBeValueFromRead()
		{
			addressMode.Run();

			cpu.AbsoluteAddress.Should().Be(ReadValue);
		}
	}
}