using System;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AbsoluteXOffset : Absolute
	{
		public override string Name => "Absolute,X";

		public AbsoluteXOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var cycles = base.Run();

			cpu.AbsoluteAddress += cpu.XRegister;
			
			return cycles;
		}
	}
}