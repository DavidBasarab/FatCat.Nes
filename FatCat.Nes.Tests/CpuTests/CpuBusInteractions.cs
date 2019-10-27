using FluentAssertions;
using Moq;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuBusInteractions
	{
		private readonly Mock<IBus> bus;
		private readonly Cpu cpu;

		public CpuBusInteractions()
		{
			bus = new Mock<IBus>();

			cpu = new Cpu(bus.Object);
		}

		[Fact]
		public void WillReadFromBus()
		{
			ushort address = 0x14b2;

			cpu.Read(address);

			bus.Verify(v => v.Read(address));
		}

		[Fact]
		public void WillReturnValueReadFromTheBus()
		{
			byte result = 0x34;

			ushort address = 0x98bc;

			bus.Setup(v => v.Read(address)).Returns(result);

			var cpuResult = cpu.Read(address);

			cpuResult.Should().Be(result);
		}

		[Fact]
		public void WillWriteDataToTheBus()
		{
			ushort address = 0x67fe;
			byte data = 0xac;

			cpu.Write(address, data);

			bus.Verify(v => v.Write(address, data));
		}
	}
}