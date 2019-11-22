namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectXMode : AddressMode
	{
		private byte highAddress;
		private byte location;
		private byte lowAddress;

		public override string Name => "(Indirect,X)";

		public IndirectXMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			ReadLocation();

			ReadLowAddress();

			ReadHighAddress();

			SetAbsoluteAddress(highAddress, lowAddress);

			return 0;
		}

		private void ReadHighAddress()
		{
			var highLocation = (ushort)(location + 1).ApplyLowMask();

			highAddress = cpu.Read(highLocation);
		}

		private void ReadLocation()
		{
			location = ReadProgramCounter();

			location += cpu.XRegister;
		}

		private void ReadLowAddress()
		{
			var lowLocation = (ushort)location.ApplyLowMask();

			lowAddress = cpu.Read(lowLocation);
		}
	}
}