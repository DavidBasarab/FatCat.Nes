using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public class LoadXRegister : LoadingValue
	{
		public override string Name => "LDX";

		public LoadXRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		protected override void SetFetchedValue() => cpu.XRegister = fetched;
	}
}