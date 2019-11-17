namespace FatCat.Nes.OpCodes.AddressingModes
{
	public interface IAddressMode
	{
		string Name { get; }

		int Run();

		byte Fetch();
	}

	public abstract class AddressMode : IAddressMode
	{
		protected readonly ICpu cpu;

		public abstract string Name { get; }

		protected AddressMode(ICpu cpu) => this.cpu = cpu;

		public abstract int Run();

		public virtual byte Fetch()
		{
			cpu.Read(cpu.AbsoluteAddress);
			
			return 0;
		}

		protected void IncrementProgramCounter() => cpu.ProgramCounter++;

		protected byte ReadProgramCounter()
		{
			var readValue = cpu.Read(cpu.ProgramCounter);

			cpu.ProgramCounter++;

			return readValue;
		}

		protected void SetAbsoluteAddress(byte high, byte low) => cpu.AbsoluteAddress = (ushort)((high << 8) | low);
	}
}