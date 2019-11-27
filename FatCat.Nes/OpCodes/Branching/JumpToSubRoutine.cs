using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Branching
{
	public class JumpToSubRoutine : OpCode
	{
		public override string Name => "JSR";

		public JumpToSubRoutine(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.ProgramCounter--;

			PushToStack((cpu.ProgramCounter >> 8).ApplyLowMask());
			PushToStack(cpu.ProgramCounter.ApplyLowMask());

			cpu.ProgramCounter = cpu.AbsoluteAddress;

			return 0;
		}
	}
}