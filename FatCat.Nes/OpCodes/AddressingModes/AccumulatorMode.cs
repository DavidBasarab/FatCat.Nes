namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AccumulatorMode : ImpliedAddressMode
	{
		public override string Name => "Accumulator";

		public AccumulatorMode(ICpu cpu) : base(cpu) { }
	}
}