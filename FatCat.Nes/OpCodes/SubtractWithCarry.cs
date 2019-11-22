using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class SubtractWithCarry : WithCarryOpCode
	{
		public override string Name => "SBC";

		public SubtractWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			fetchedData = addressMode.Fetch();

			fetchedData = (ushort)(fetchedData ^ 0x00ff);

			return DoAddWithCarry();
		}
	}
}