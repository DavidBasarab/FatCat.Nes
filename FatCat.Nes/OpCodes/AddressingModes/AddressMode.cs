namespace FatCat.Nes.OpCodes.AddressingModes
{
	public abstract class AddressMode
	{
		protected readonly ICpu cpu;

		public abstract string Name { get; }

		protected AddressMode(ICpu cpu) => this.cpu = cpu;

		public abstract int Run();

		protected void IncrementProgramCounter() => cpu.ProgramCounter++;

		protected byte ReadProgramCounter()
		{
			var readValue = cpu.Read(cpu.ProgramCounter);

			cpu.ProgramCounter++;

			return readValue;
		}
	}
}