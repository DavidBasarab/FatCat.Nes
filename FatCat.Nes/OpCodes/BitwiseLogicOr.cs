using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BitwiseLogicOr : OpCode
	{
		public BitwiseLogicOr(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "ORA";

		public override int Execute()
		{
			Fetch();
			
			return -1;
		}
	}
}