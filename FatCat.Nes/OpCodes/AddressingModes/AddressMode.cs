namespace FatCat.Nes.OpCodes.AddressingModes
{
	public abstract class AddressMode
	{
		protected readonly ICpu cpu;

		public abstract string Name { get; }

		protected AddressMode(ICpu cpu) => this.cpu = cpu;

		public abstract int Run();
	}
}