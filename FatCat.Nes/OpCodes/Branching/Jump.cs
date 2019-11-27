using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Branching
{
	public class Jump : OpCode
	{
		public override string Name => "JMP";

		public Jump(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.ProgramCounter = cpu.AbsoluteAddress;

			return 0;
		}
	}
}