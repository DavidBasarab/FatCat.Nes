namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ImmediateMode : AddressMode
	{
		public ImmediateMode(ICpu cpu) : base(cpu) { }

		public override string Name => "Immediate";

		public override int Run()
		{
			cpu.AbsoluteAddress = cpu.ProgramCounter;

			IncrementProgramCounter();

			return 0;
		}

		
	}
}