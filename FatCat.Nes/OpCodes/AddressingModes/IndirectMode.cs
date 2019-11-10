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
			lowPointer = ReadProgramCounter();
			highPointer = ReadProgramCounter();

			pointer = (ushort)((highPointer << 8) | lowPointer);

			if (IsPageBoundary)
			{
				lowAddress = cpu.Read(pointer);

				var highBoundaryPointer = pointer & 0xff00;

				highAddress = cpu.Read((ushort)highBoundaryPointer);
			}
			else
			{
				lowAddress = cpu.Read(pointer);
				highAddress = cpu.Read((ushort)(pointer + 1));
			}

			cpu.AbsoluteAddress = (ushort)((highAddress << 8) | lowAddress);

			return 0;
		}
	}
}