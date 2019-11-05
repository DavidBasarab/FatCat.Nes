using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImpliedAddressModeTests
	{
		private readonly ImpliedAddressMode addressMode;

		public ImpliedAddressModeTests() => addressMode = new ImpliedAddressMode();

		[Fact]
		public void WillHaveNameOfImplied() => addressMode.Name.Should().Be("Implied");
	}
}