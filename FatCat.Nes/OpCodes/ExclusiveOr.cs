using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ExclusiveOr : OpCode
	{
		public ExclusiveOr(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "EOR";

		public override int Execute()
		{
			Fetch();

			cpu.Accumulator = (byte)(cpu.Accumulator ^ fetched);
			
			return -1;
		}
	}
}