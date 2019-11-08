namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class Relative : AddressMode
	{
		public override string Name => "Relative";

		public Relative(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.RelativeAddress = ReadProgramCounter();

			if ((cpu.RelativeAddress & 0x80) > 0) cpu.RelativeAddress = (ushort)(cpu.RelativeAddress | 0xff00);

			return 0;
		}
	}
}