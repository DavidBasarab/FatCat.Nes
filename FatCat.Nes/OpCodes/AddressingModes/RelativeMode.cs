namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class RelativeMode : AddressMode
	{
		public override string Name => "Relative";

		public RelativeMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.RelativeAddress = ReadProgramCounter();

			if ((cpu.RelativeAddress & 0x80) > 0) cpu.RelativeAddress = (ushort)(cpu.RelativeAddress | 0xff00);

			return 0;
		}
	}
}