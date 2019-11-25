using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class CompareAccumulator : OpCode
	{
		public CompareAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "CMP";

		public override int Execute() => -1;
	}
}