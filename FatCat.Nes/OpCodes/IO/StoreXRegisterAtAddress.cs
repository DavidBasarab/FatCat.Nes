using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class StoreXRegisterAtAddress : OpCode
	{
		public override string Name => "STX";

		public StoreXRegisterAtAddress(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.Write(cpu.AbsoluteAddress, cpu.XRegister);

			return 0;
		}
	}
}