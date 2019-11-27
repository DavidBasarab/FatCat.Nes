using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PushAccumulator : OpCode
	{
		public override string Name => "PHA";

		public PushAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			PushToStack(cpu.Accumulator);

			return 0;
		}
	}
}