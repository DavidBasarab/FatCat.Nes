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

			var lowAddress = cpu.Read((ushort)(readValue & 0x00ff));
			var highAddress = cpu.Read((ushort)((readValue + 1) & 0x00ff));
			
			SetAbsoluteAddress(highAddress, lowAddress);

			cpu.AbsoluteAddress += cpu.YRegister;
			
			return 0;
		}
	}
}