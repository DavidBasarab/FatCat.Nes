using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public abstract class BranchOpCode : OpCode
	{
		protected abstract CpuFlag Flag { get; }
		
		protected abstract bool FlagState { get; }

		protected BranchOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var cycles = 0;

			var flagSet = cpu.GetFlag(Flag);

			if (flagSet == FlagState)
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