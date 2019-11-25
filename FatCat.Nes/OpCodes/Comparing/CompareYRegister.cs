using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Comparing
{
	public class CompareYRegister : CompareOpcode
	{
		public override string Name => "CPY";

		protected override byte RegisterValue => cpu.YRegister;

		public CompareYRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}