using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests
{
	public class BusTests
	{
		private readonly Bus bus;

		public BusTests() => bus = new Bus();

		[Fact]
		public void AllRamSetToZero()
		{
			foreach (var data in bus.Ram) data.Should().Be(0x00);
		}

		[Theory]
		[InlineData(0x0000, 0x7f)]
		[InlineData(0x0A00, 0x4f)]
		[InlineData(0x0AB0, 0x43)]
		public void CanReadAndWriteToBus(ushort address, byte data)
		{
			bus.Write(address, data);

			var result = bus.Read(address);

			result.Should().Be(data);
		}

		[Fact]
		public void RamLengthWillBe64K() => bus.Ram.Length.Should().Be(64 * 1024);

		[Theory]
		[InlineData(0x0000, 0x74, 0x11)]
		public void WillWriteLastDataToRam(ushort address, byte firstData, byte secondData)
		{
			bus.Write(address, firstData);
			bus.Write(address, secondData);

			var currentMemory = bus.Read(address);

			currentMemory.Should().Be(secondData);
		}

		[Fact]
		public void WillWriteValuesToMemory()
		{
			bus.Write(0x674, 0x21);
			bus.Write(0x789, 0x85);

			bus.Read(0x674).Should().Be(0x21);
			bus.Read(0x789).Should().Be(0x85);
		}
	}
}