namespace FatCat.Nes
{
	public class Bus
	{
		internal byte[] Ram { get; } = new byte[64 * 1024];

		public byte Read(ushort address) => Ram[address];

		public void Write(ushort address, byte data) => Ram[address] = data;
	}
}