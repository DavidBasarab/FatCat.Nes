using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public class LoadYRegister : LoadingValue
	{
		public override string Name => "LDY";

		public LoadYRegister(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		protected override void SetFetchedValue() => cpu.YRegister = fetched;
	}
}