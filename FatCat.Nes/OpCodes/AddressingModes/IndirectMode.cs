namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectMode : AddressMode
	{
		public override string Name => "Indirect";

		public IndirectMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var lowPointer = ReadProgramCounter();
			var highPointer = ReadProgramCounter();

			var pointer = (ushort)((highPointer << 8) | lowPointer);

			var lowAddress = cpu.Read(pointer);
			var highAddress = cpu.Read((ushort)(pointer + 1));

			cpu.AbsoluteAddress = (ushort)((highAddress << 8) | lowAddress);

			return 0;
		}
	}
}