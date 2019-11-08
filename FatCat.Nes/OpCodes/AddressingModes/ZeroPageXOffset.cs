using System;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ZeroPageXOffset : AddressMode
	{
		public override string Name => "ZeroPage,X";

		public ZeroPageXOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.Read(cpu.ProgramCounter);

			return -1;
		}
	}
}