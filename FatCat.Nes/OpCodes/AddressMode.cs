namespace FatCat.Nes.OpCodes
{
	public abstract class AddressMode
	{
		public abstract string Name { get; }

		public abstract int Run();
	}

	public class ImpliedAddressMode : AddressMode
	{
		public override string Name => "Implied";

		public override int Run() => -1;
	}
}