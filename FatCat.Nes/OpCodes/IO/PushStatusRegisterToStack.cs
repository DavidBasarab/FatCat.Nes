using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PushStatusRegisterToStack : OpCode
	{
		public PushStatusRegisterToStack(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "PHP";

		public override int Execute() => -1;
	}
}