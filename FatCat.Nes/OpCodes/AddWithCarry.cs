using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class AddWithCarry : OpCode
	{
		public AddWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var fetchedData = addressMode.Fetch();

			var carryFlag = cpu.GetFlag(CpuFlag.CarryBit);

			var total = cpu.Accumulator + fetchedData + (carryFlag ? 1 : 0);

			if (total > 255) cpu.SetFlag(CpuFlag.CarryBit);
			else cpu.RemoveFlag(CpuFlag.CarryBit);

			if ((total & 0x00ff) == 0) cpu.SetFlag(CpuFlag.Zero);
			else cpu.RemoveFlag(CpuFlag.Zero);

			// Overflow Flag = ~(A^F) & (A^T)
			var accumulatorFetched = cpu.Accumulator ^ fetchedData;
			var accumulatorTotal = cpu.Accumulator ^ total;

			var shouldOverflow = ((~accumulatorFetched & accumulatorTotal) & 0x0080) > 0;

			if (shouldOverflow) cpu.SetFlag(CpuFlag.Overflow);
			else cpu.RemoveFlag(CpuFlag.Overflow);

			return -1;
		}
	}
}