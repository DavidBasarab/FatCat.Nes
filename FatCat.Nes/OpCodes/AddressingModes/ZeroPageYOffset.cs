namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ZeroPageYOffset : AddressMode
	{
		public override string Name => "ZeroPage,Y";

		public ZeroPageYOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var readValue = ReadProgramCounter();

			cpu.AbsoluteAddress = (ushort)(readValue + cpu.YRegister);

			cpu.AbsoluteAddress &= 0x00ff;

			return 0;
		}
	}
}