using FakeItEasy;
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
			var bus = A.Fake<IBus>();
			
			var cpu = new Cpu(bus);
			
			addressMode = new ImpliedAddressMode(cpu);
		}

		[Fact]
		public void WillHaveNameOfImplied() => addressMode.Name.Should().Be("Implied");
	}
}