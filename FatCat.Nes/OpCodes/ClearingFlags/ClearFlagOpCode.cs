using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.ClearingFlags
{
	public abstract class ClearFlagOpCode : OpCode
	{
		protected abstract CpuFlag Flag { get; }

		protected ClearFlagOpCode(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.RemoveFlag(Flag);

			return 0;
		}
	}
}