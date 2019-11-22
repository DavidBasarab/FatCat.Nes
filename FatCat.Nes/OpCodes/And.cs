using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class And : OpCode
	{
		public And(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "AND";

		public override int Execute()
		{
			var fetched = addressMode.Fetch();

			cpu.Accumulator = (byte)(cpu.Accumulator & fetched);
			
			return -1;
		}
	}
}