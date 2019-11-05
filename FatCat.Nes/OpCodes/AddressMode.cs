namespace FatCat.Nes.OpCodes
{
	public abstract class AddressMode
	{
		protected readonly Cpu cpu;

		public abstract string Name { get; }

		protected AddressMode(Cpu cpu) => this.cpu = cpu;

		public abstract int Run();
	}

	public class ImpliedAddressMode : AddressMode
	{
		public override string Name => "Implied";

		public ImpliedAddressMode(Cpu cpu) : base(cpu) { }

		public override int Run() => -1;
	}
}