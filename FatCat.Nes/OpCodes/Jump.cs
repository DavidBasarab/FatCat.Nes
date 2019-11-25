using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class Jump : OpCode
	{
		public Jump(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "JMP";

		public override int Execute()
		{
			cpu.ProgramCounter = cpu.AbsoluteAddress;
			
			return -1;
		}
	}
}