using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ArithmeticShiftLeft : OpCode
	{
		public override string Name => "ASL";

		public ArithmeticShiftLeft(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();
			
			return -1;
		}
	}
}