using System;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class IndirectMode : AddressMode
	{
		public override string Name => "Indirect";

		public IndirectMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			return 0;
		}
	}
}