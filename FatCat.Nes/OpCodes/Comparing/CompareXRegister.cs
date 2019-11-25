using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Comparing
{
	public class CompareXRegister : CompareOpcode
	{
		public override string Name => "CPX";

		protected override byte RegisterValue => cpu.XRegister;

		public CompareXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}