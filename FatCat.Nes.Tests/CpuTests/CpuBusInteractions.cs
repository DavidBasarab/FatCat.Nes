using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.CpuTests
{
	public class CpuBusInteractions : CpuBaseTests
	{
		[Fact]
		public void WillReadFromBus()
		{
			ushort address = 0x14b2;

			cpu.Read(address);

			A.CallTo(() => bus.Read(address)).MustHaveHappened();
		}

		[Fact]
		public void WillReturnValueReadFromTheBus()
		{
			byte result = 0x34;

			ushort address = 0x98bc;

			A.CallTo(() => bus.Read(address)).Returns(result);

			var cpuResult = cpu.Read(address);

			cpuResult.Should().Be(result);
		}

		[Fact]
		public void WillWriteDataToTheBus()
		{
			ushort address = 0x67fe;
			byte data = 0xac;

			cpu.Write(address, data);

			A.CallTo(() => bus.Write(address, data)).MustHaveHappened();
		}
	}
}