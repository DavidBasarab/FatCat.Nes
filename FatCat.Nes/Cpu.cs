namespace FatCat.Nes
{
	public class Cpu
	{
		private readonly IBus bus;

		public Cpu(IBus bus) => this.bus = bus ?? new Bus();

		public byte Read(ushort address) => bus.Read(address);

		public void Write(ushort address, byte data) => bus.Write(address, data);
	}
}