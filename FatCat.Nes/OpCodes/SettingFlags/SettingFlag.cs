using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.SettingFlags
{
	public abstract class SettingFlag : OpCode
	{
		protected abstract CpuFlag Flag { get; }

		protected SettingFlag(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.SetFlag(Flag);

			return 0;
		}
	}
}