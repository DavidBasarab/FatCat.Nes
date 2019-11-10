namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectModeX : AddressMode
	{
		public override string Name => "(Indirect,X)";

		public IndirectModeX(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var value = ReadProgramCounter();
			
			var lowLocation = (ushort)((value + cpu.XRegister) & 0x00ff);

			var lowAddress = cpu.Read(lowLocation);

			return 0;
		}
	}
}