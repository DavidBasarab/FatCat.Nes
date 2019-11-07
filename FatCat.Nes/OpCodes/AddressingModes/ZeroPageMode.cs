namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ZeroPageMode : AddressMode
	{
		public override string Name => "ZeroPage";

		public ZeroPageMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.Read(cpu.ProgramCounter);
			
			return -1;
		}
	}
}