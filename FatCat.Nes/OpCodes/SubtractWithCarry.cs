using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class SubtractWithCarry : WithCarryOpCode
	{
		public override string Name => "SBC";

		public SubtractWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			fetched = (byte)(fetched ^ 0x00ff);

			return DoAddWithCarry();
		}
	}
}