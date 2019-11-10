namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectYMode : AddressMode
	{
		private byte highAddress;
		private byte lowAddress;

		public override string Name => "(Indirect),Y";

		private bool HasPaged
		{
			get
			{
				var highPart = cpu.AbsoluteAddress & 0xff00;

				return highPart != highAddress << 8;
			}
		}

		public IndirectYMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var readValue = ReadProgramCounter();

			lowAddress = cpu.Read((ushort)(readValue & 0x00ff));
			highAddress = cpu.Read((ushort)((readValue + 1) & 0x00ff));

			SetAbsoluteAddress(highAddress, lowAddress);

			cpu.AbsoluteAddress += cpu.YRegister;

			return HasPaged ? 1 : 0;
		}
	}
}