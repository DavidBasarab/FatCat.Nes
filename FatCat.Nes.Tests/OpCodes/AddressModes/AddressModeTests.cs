using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public abstract class AddressModeTests
	{
		protected readonly ICpu cpu;

		protected AddressMode addressMode;

		protected abstract string ExpectedName { get; }

		protected AddressModeTests() => cpu = A.Fake<ICpu>();

		[Fact]
		public void WillHaveCorrectAddressModeName() => addressMode.Name.Should().Be(ExpectedName);
	}
}