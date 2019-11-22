namespace FatCat.Nes
{
	public static class BitExtensions
	{
		public static byte ApplyLowMask(this ushort value) => (byte)(value & 0x00ff);

		public static byte ApplyLowMask(this byte value) => (byte)(value & 0x00ff);

		public static byte ApplyLowMask(this int value) => (byte)(value & 0x00ff);

		public static bool IsZero(this byte value) => value == 0x00;
	}
}