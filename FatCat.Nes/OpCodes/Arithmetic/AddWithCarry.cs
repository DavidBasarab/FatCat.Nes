using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class AddWithCarry : WithCarryOpCode
	{
		public override string Name => "ADC";

		public AddWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			return DoAddWithCarry();
		}
	}
}