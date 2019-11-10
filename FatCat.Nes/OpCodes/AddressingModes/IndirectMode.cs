namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectMode : AddressMode
	{
		private byte highAddress;
		private byte highPointer;
		private byte lowAddress;
		private byte lowPointer;
		private ushort pointer;

		public override string Name => "Indirect";

		private bool IsPageBoundary => lowPointer == 0xff;

		public IndirectMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			ReadPointer();

			lowAddress = cpu.Read(pointer);

			ReadHighAddress();

			cpu.AbsoluteAddress = (ushort)((highAddress << 8) | lowAddress);

			return 0;
		}

		private void ReadHighAddress()
		{
			var highAddressPointer = IsPageBoundary ? (ushort)(pointer & 0xff00) : (ushort)(pointer + 1);

			highAddress = cpu.Read(highAddressPointer);
		}

		private void ReadPointer()
		{
			lowPointer = ReadProgramCounter();
			highPointer = ReadProgramCounter();

			pointer = (ushort)((highPointer << 8) | lowPointer);
		}
	}
}