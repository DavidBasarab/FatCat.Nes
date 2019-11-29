using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class StoreAccumulatorAtAddress : OpCode
	{
		public StoreAccumulatorAtAddress(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "STA";

		public override int Execute()
		{
			cpu.Write(cpu.AbsoluteAddress, cpu.Accumulator);
			
			return 0;
		}
	}
}