using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ReturnFromInterrupt : OpCode
	{
		public override string Name => "RTI";

		public ReturnFromInterrupt(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			_ = ReadFromStack();

			return -1;
		}
	}
}