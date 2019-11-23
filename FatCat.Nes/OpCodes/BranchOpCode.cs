using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public abstract class BranchOpCode : OpCode
	{
		protected BranchOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		protected int PerformBranch(CpuFlag flag)
		{
			var cycles = 0;

			var carrySet = cpu.GetFlag(flag);

			if (carrySet)
			{
				cycles++;

				cpu.AbsoluteAddress = (ushort)(cpu.ProgramCounter + cpu.RelativeAddress);

				if (HasPaged()) cycles++;

				cpu.ProgramCounter = cpu.AbsoluteAddress;
			}

			return cycles;
		}

		private bool HasPaged() => cpu.AbsoluteAddress.ApplyHighMask() != cpu.ProgramCounter.ApplyHighMask();
	}
}