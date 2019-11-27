using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public abstract class WithCarryOpCode : OpCode
	{
		protected bool carryFlag;
		protected int total;

		protected WithCarryOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		protected int DoAddWithCarry()
		{
			carryFlag = cpu.GetFlag(CpuFlag.CarryBit);

			total = cpu.Accumulator + fetched + (carryFlag ? 1 : 0);

			ApplyFlag(CpuFlag.CarryBit, total.HasCarried());

			ApplyFlag(CpuFlag.Negative, total.IsNegative());

			ApplyFlag(CpuFlag.Zero, total.IsZero());

			SetOverflowFlag();

			cpu.Accumulator = total.ApplyLowMask();

			return 1;
		}

		private void SetOverflowFlag()
		{
			// Overflow Flag = ~(A^F) & (A^T)
			var accumulatorFetched = cpu.Accumulator ^ fetched;
			var accumulatorTotal = cpu.Accumulator ^ total;

			var shouldOverflow = (~accumulatorFetched & accumulatorTotal & 0x0080) > 0;

			if (shouldOverflow) cpu.SetFlag(CpuFlag.Overflow);
			else cpu.RemoveFlag(CpuFlag.Overflow);
		}
	}
}