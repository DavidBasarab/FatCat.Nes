using FakeItEasy;

namespace FatCat.Nes.Tests.CpuTests
{
	public abstract class CpuBaseTests
	{
		protected readonly IBus bus;
		protected readonly Cpu cpu;

		protected CpuBaseTests()
		{
			bus = A.Fake<IBus>();

			cpu = new Cpu(bus);
		}
	}
}