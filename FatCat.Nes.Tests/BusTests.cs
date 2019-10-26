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

		[Theory]
		[InlineData(0x0000, 0x7f)]
		public void CanDoStuff(ushort address, byte data)
		{
			bus.Write(address, data);

			var result = bus.Read(address);

			result.Should().Be(data);
		}
	}
}