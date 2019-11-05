using FatCat.Nes.OpCodes;
using FluentAssertions;
using Moq;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class ImpliedAddressModeTests
	{
		private readonly ImpliedAddressMode addressMode;

		public ImpliedAddressModeTests()
		{
			var bus = new Mock<IBus>();
			
			var cpu = new Cpu(bus.Object);
			
			addressMode = new ImpliedAddressMode(cpu);
		}

		[Fact]
		public void WillHaveNameOfImplied() => addressMode.Name.Should().Be("Implied");
	}
}