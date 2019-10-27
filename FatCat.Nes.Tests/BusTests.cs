using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests
{
	public class BusTests
	{
		public static IEnumerable<object[]> MemoryTestData
		{
			get
			{
				yield return new object[] { 0x674, 0x21 };
				yield return new object[] { 0x789, 0x85 };
				yield return new object[] { 0x0000, 0x38 };
				yield return new object[] { 0x0038, 0x07 };
				yield return new object[] { 0x0053, 0x03 };
			}
		}

		private readonly Bus bus;

		public BusTests() => bus = new Bus();

		[Fact]
		public void AllRamSetToZero() => VerifyAllMemoryIsZero();

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
		[MemberData(nameof(MemoryTestData), MemberType = typeof(BusTests))]
		public void WillReadDataFromCorrectLocationInMemory(ushort address, byte data)
		{
			bus.Ram[address] = data;

			bus.Read(address).Should().Be(data);
		}

		[Theory]
		[MemberData(nameof(MemoryTestData), MemberType = typeof(BusTests))]
		public void WillWriteDataToCorrectLocationInMemory(ushort address, byte data)
		{
			bus.Write(address, data);

			bus.Ram[address].Should().Be(data);
		}

		[Theory]
		[InlineData(0x0000, 0x74, 0x11)]
		public void WillWriteLastDataToRam(ushort address, byte firstData, byte secondData)
		{
			bus.Write(address, firstData);
			bus.Write(address, secondData);

			var currentMemory = bus.Read(address);

			currentMemory.Should().Be(secondData);
		}

		private void VerifyAllMemoryIsZero()
		{
			foreach (var data in bus.Ram) data.Should().Be(0x00);
		}
	}
}