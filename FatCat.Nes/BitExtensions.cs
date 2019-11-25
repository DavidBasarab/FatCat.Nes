namespace FatCat.Nes
{
	public static class BitExtensions
	{
		private const int HighMask = 0xff00;
		private const int LowMask = 0x00ff;

		public static ushort ApplyHighMask(this ushort value) => (ushort)(value & HighMask);

		public static byte ApplyHighMask(this byte value) => (byte)(value & HighMask);

		public static byte ApplyHighMask(this int value) => (byte)(value & HighMask);

		public static byte ApplyLowMask(this ushort value) => (byte)(value & LowMask);

		public static byte ApplyLowMask(this byte value) => (byte)(value & LowMask);

		public static byte ApplyLowMask(this int value) => (byte)(value & LowMask);

		public static bool HasCarried(this ushort value) => (value & HighMask) > 0;

		public static bool HasCarried(this int value) => (value & HighMask) > 0;

		public static bool IsNegative(this byte value) => (value & 0x80) > 0;

		public static bool IsNegative(this int value) => (value & 0x80) > 0;

		public static bool IsNegative(this ushort value) => (value & 0x80) > 0;

		public static bool IsZero(this byte value) => value == 0x00;

		public static bool IsZero(this ushort value) => value.ApplyLowMask() == 0x00;

		public static bool IsZero(this int value) => value == 0x00;
	}
}