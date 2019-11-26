using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class JumpToSubRoutine : OpCode
	{
		public override string Name => "JSR";

		public JumpToSubRoutine(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.ProgramCounter--;

			WriteToStack((cpu.ProgramCounter >> 8).ApplyLowMask());
			WriteToStack(cpu.ProgramCounter.ApplyLowMask());

			return -1;
		}
	}
}