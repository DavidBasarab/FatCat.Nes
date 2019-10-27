namespace FatCat.Nes
{
	public class Cpu
	{
		private readonly IBus bus;

		private CpuFlag flag;

		public Cpu(IBus bus) => this.bus = bus ?? new Bus();

		public bool GetFlag(CpuFlag cpuFlag) => flag.HasFlag(cpuFlag);

		public byte Read(ushort address) => bus.Read(address);

		public void SetFlag(CpuFlag cpuFlag) => flag |= cpuFlag;

		public void Write(ushort address, byte data) => bus.Write(address, data);

		public void RemoveFlag(CpuFlag cpuFlag) => flag &= ~cpuFlag;
	}
}