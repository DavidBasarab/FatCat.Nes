using FakeItEasy;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImpliedAddressModeTests
	{
		private readonly ImpliedAddressMode addressMode;

		public ImpliedAddressModeTests()
		{
			var cpu = A.Fake<ICpu>();

			addressMode = new ImpliedAddressMode(cpu);
		}

		[Fact]
		public void WillHaveNameOfImplied() => addressMode.Name.Should().Be("Implied");
	}
}