namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class Absolute : AddressMode
	{
		protected byte high;
		protected byte low;

		public override string Name => "Absolute";

		public Absolute(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			low = ReadProgramCounter();
			high = ReadProgramCounter();

			cpu.AbsoluteAddress = (ushort)((high << 8) | low);

			return 0;
		}
	}
}