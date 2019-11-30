using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class IllegalOpCode : OpCode
	{
		public override string Name => "XXX";

		public IllegalOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => 0;
	}
}