using System;

namespace FatCat.Nes
{
	public class Bus
	{
		internal int[] Ram { get; } = new int[64 * 1024];

		private byte data;

		public byte Read(ushort address) => data;

		public void Write(ushort address, byte data) { this.data = data; }
	}
}