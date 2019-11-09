using System;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class Absolute : AddressMode
	{
		public override string Name => "Absolute";

		public Absolute(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var low = ReadProgramCounter();
			
			return 0;
		}
	}
}