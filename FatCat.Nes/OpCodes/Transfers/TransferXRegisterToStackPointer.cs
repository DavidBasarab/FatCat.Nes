using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Transfers
{
	public class TransferXRegisterToStackPointer : OpCode
	{
		public override string Name => "TXS";

		public TransferXRegisterToStackPointer(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.StackPointer = cpu.XRegister;

			return 0;
		}
	}
}