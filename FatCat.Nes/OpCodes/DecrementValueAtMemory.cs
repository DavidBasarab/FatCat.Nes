using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class DecrementValueAtMemory : OpCode
	{
		public DecrementValueAtMemory(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "DEC";

		public override int Execute()
		{
			Fetch();

			var value = fetched - 1;
			
			cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());
			
			return -1;
		}
	}
}