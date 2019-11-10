namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectModeX : AddressMode
	{
		public override string Name => "(Indirect,X)";

		public IndirectModeX(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var location = ReadProgramCounter();

			location += cpu.XRegister;
			
			var lowLocation = (ushort)((location) & 0x00ff);

			var lowAddress = cpu.Read(lowLocation);

			ushort highLocation = (ushort)((location + 1) & 0x00ff);

			var highAddress = cpu.Read(highLocation);

			return 0;
		}
	}
}