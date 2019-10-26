namespace FatCat.Nes
{
	public class Bus
	{
		internal byte[] Ram { get; } = new byte[64 * 1024];

		public byte Read(ushort address)
		{
			return tempData;

			//return Ram[address];
		}

		private byte tempData;

		public void Write(ushort address, byte data)
		{
			tempData = data;
			
			Ram[address] = data;
		}
	}
}