using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class AddWithCarry : WithCarryOpCode
	{
		public override string Name => "ADC";

		public AddWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			fetchedData = addressMode.Fetch();

			return DoAddWithCarry();
		}
	}
}