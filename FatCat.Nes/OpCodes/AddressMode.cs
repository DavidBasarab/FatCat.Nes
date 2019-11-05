using System.Drawing;

namespace FatCat.Nes.OpCodes
{
	public abstract class AddressMode
	{
		protected readonly Cpu cpu;

		protected AddressMode(Cpu cpu) { this.cpu = cpu; }
		
		public abstract string Name { get; }

		public abstract int Run();
	}

	public class ImpliedAddressMode : AddressMode
	{
		public override string Name => "Implied";

		public override int Run() => -1;

		public ImpliedAddressMode(Cpu cpu) : base(cpu) { }
	}
}