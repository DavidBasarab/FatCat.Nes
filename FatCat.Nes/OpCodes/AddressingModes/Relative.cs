namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class Relative : AddressMode
	{
		public override string Name => "Relative";

		public Relative(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			var readValue = ReadProgramCounter();

			return 0;
		}

		
	}
}