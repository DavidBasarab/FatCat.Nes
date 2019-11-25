using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Comparing
{
	public class CompareAccumulator : CompareOpcode
	{
		public override string Name => "CMP";

		protected override byte RegisterValue => cpu.Accumulator;

		public CompareAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}