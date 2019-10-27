using Moq;

namespace FatCat.Nes.Tests.CpuTests
{
	public abstract class CpuBaseTests
	{
		protected readonly Mock<IBus> bus;
		protected readonly Cpu cpu;

		protected CpuBaseTests()
		{
			bus = new Mock<IBus>();

			cpu = new Cpu(bus.Object);
		}
	}
}