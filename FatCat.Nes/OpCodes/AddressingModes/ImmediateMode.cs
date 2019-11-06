namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class ImmediateMode : AddressMode
	{
		public ImmediateMode(ICpu cpu) : base(cpu) { }

		public override string Name { get; }

		public override int Run() => throw new System.NotImplementedException();
	}
}