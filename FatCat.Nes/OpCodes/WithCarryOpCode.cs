using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public abstract class WithCarryOpCode : OpCode
	{
		protected bool carryFlag;
		protected ushort fetchedData;
		protected int total;

		protected WithCarryOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		protected int DoAddWithCarry()
		{
			carryFlag = cpu.GetFlag(CpuFlag.CarryBit);

			total = cpu.Accumulator + fetchedData + (carryFlag ? 1 : 0);

			SetCarryBit();

			SetZeroFlag();

			SetOverflowFlag();

			SetCarryFlag();

			cpu.Accumulator = total.ApplyLowMask();

			return 1;
		}

		private void SetCarryBit()
		{
			if (total > 255) cpu.SetFlag(CpuFlag.CarryBit);
			else cpu.RemoveFlag(CpuFlag.CarryBit);
		}

		private void SetCarryFlag()
		{
			var negativeFlag = (total & 0x80) > 0;

			if (negativeFlag) cpu.SetFlag(CpuFlag.Negative);
			else cpu.RemoveFlag(CpuFlag.Negative);
		}

		private void SetOverflowFlag()
		{
			// Overflow Flag = ~(A^F) & (A^T)
			var accumulatorFetched = cpu.Accumulator ^ fetchedData;
			var accumulatorTotal = cpu.Accumulator ^ total;

			var shouldOverflow = (~accumulatorFetched & accumulatorTotal & 0x0080) > 0;

			if (shouldOverflow) cpu.SetFlag(CpuFlag.Overflow);
			else cpu.RemoveFlag(CpuFlag.Overflow);
		}

		private void SetZeroFlag()
		{
			if (total.ApplyLowMask() == 0) cpu.SetFlag(CpuFlag.Zero);
			else cpu.RemoveFlag(CpuFlag.Zero);
		}
	}
}