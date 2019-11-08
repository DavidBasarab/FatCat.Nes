using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public abstract class AddressModeTests
	{
		protected const ushort ProgramCounter = 0xef2;
		protected const byte ReadValue = 0x38;

		protected readonly ICpu cpu;

		protected AddressMode addressMode;

		protected abstract int ExpectedCycles { get; }

		protected abstract string ExpectedName { get; }

		protected AddressModeTests()
		{
			cpu = A.Fake<ICpu>();

			cpu.ProgramCounter = ProgramCounter;

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(ReadValue);
		}

		[Fact]
		public void RunWillTakeCycles()
		{
			var cycles = addressMode.Run();

			cycles.Should().Be(ExpectedCycles);
		}

		[Fact]
		public void WillHaveCorrectAddressModeName() => addressMode.Name.Should().Be(ExpectedName);
	}
}