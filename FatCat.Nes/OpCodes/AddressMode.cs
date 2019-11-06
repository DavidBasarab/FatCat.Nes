namespace FatCat.Nes.OpCodes
{
	public abstract class AddressMode
	{
		protected readonly ICpu cpu;

		public abstract string Name { get; }

		protected AddressMode(ICpu cpu) => this.cpu = cpu;

		public abstract int Run();
	}

	public class ImpliedAddressMode : AddressMode
	{
		public override string Name => "Implied";

		public ImpliedAddressMode(ICpu cpu) : base(cpu) { }

		public override int Run() => -1;
	}
}