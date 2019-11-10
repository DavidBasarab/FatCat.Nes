namespace FatCat.Nes.OpCodes.AddressingModes
{
	/// <summary>
	///  Address Mode: Indirect
	///  The supplied 16-bit address is read from to get the actual 16-bit address. This
	///  instruction is unusual in that it has a bug in the hardware! To emulate the
	///  function accurately, we also need to emulate this bug. If the low byte of the
	///  supplied address is 0xFF, then to read the high byte of the actual address
	///  we need to cross a page boundary. This doesn't actually work on the chip as
	///  designed, instead it wraps back around in the same page, yielding an
	///  invalid actual address
	/// </summary>
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