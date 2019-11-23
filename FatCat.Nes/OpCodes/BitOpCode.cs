using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BitOpCode : OpCode
	{
		public BitOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "BIT";

		public override int Execute() => -1;
	}
}