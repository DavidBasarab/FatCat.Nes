using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class StoreYRegisterAtAddress : OpCode
	{
		public override string Name => "STY";

		public StoreYRegisterAtAddress(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.Write(cpu.AbsoluteAddress, cpu.YRegister);

			return 0;
		}
	}
}