namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ZeroPageXOffset : AddressMode
	{
		public override string Name => "ZeroPage,X";

		public ZeroPageXOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var readValue = cpu.Read(cpu.ProgramCounter);

			cpu.AbsoluteAddress = (ushort)(readValue + cpu.XRegister);

			cpu.AbsoluteAddress &= 0x00ff;
			
			cpu.ProgramCounter++;

			return 0;
		}
	}
}