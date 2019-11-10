using System;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectYMode : AddressMode
	{
		public override string Name => "(Indirect),Y";

		public IndirectYMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var readValue = ReadProgramCounter();
			
			return 0;
		}
	}
}