namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class Absolute : AddressMode
	{
		private byte high;
		private byte low;

		public override string Name => "Absolute";

		public Absolute(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			low = ReadProgramCounter();
			high = ReadProgramCounter();

			cpu.AbsoluteAddress = (ushort)((high << 8) | low);

			return 0;
		}

		protected bool Paged()
		{
			var highAddressValue = cpu.AbsoluteAddress & 0xff00;

			return highAddressValue != high << 8;
		}
	}
}