namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AccumulatorMode : AddressMode
	{
		public override string Name => "Accumulator";

		public AccumulatorMode(ICpu cpu) : base(cpu) { }

		/// <summary>
		/// Do nothing this is because there are some opcodes that just use what is loaded in the accumulator
		/// </summary>
		/// <returns></returns>
		public override int Run() => 0;
	}
}