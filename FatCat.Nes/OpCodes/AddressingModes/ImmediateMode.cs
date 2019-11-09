namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ImmediateMode : AddressMode
	{
		public override string Name => "Immediate";

		public ImmediateMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.AbsoluteAddress = cpu.ProgramCounter;

			IncrementProgramCounter();

			return 0;
		}
	}
}