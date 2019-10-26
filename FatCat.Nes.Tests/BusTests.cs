using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests
{
	public class BusTests
	{
		private Bus bus;

		public BusTests() => bus = new Bus();

		[Fact]
		public void RamLengthWillBe65K() => bus.Ram.Length.Should().Be(64 * 1024);
	}
}