namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ImpliedAddressMode : AddressMode
	{
		public override string Name => "Implied";

		public ImpliedAddressMode(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			cpu.Fetched = cpu.Accumulator;

			return 0;
		}
	}
}