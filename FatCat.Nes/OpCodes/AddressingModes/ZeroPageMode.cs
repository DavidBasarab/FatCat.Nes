namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ZeroPageMode : AddressMode
	{
		public override string Name => "ZeroPage";

		public ZeroPageMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.AbsoluteAddress = ReadProgramCounter();

			// I don't think this is needed, but the example has it.  
			// going to leave this for now
			cpu.AbsoluteAddress &= 0x00ff;

			return 0;
		}
	}
}