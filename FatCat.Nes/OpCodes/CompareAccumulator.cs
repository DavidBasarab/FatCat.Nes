using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class CompareAccumulator : OpCode
	{
		public override string Name => "CMP";

		public CompareAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (ushort)(cpu.Accumulator - fetched);

			ApplyFlag(cpu.Accumulator >= fetched, CpuFlag.CarryBit);

			return -1;
		}
	}
}