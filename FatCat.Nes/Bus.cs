namespace FatCat.Nes
{
	public interface IBus
	{
		byte Read(ushort address);

		void Write(ushort address, byte data);
	}

	public class Bus : IBus
	{
		internal byte[] Ram { get; } = new byte[64 * 1024];

		public byte Read(ushort address) => Ram[address];

		public void Write(ushort address, byte data) => Ram[address] = data;
	}
}