using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfCarrySet : OpCode
	{
		public override string Name => "BCS";

		public BranchIfCarrySet(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var cycles = 0;

			var carrySet = cpu.GetFlag(CpuFlag.CarryBit);

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