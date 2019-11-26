using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public class LoadAccumulator : LoadingValue
	{
		public override string Name => "LDA";

		public LoadAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		protected override void SetFetchedValue() => cpu.Accumulator = fetched;
	}
}