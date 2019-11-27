using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	// TODO NOP Code

	public abstract class OpCode
	{
		protected readonly IAddressMode addressMode;
		protected readonly ICpu cpu;
		protected byte fetched;

		public abstract string Name { get; }

		protected OpCode(ICpu cpu, IAddressMode addressMode)
		{
			this.cpu = cpu;
			this.addressMode = addressMode;
		}

		public abstract int Execute();

		protected void ApplyFlag(CpuFlag flag, bool shouldSet)
		{
			if (shouldSet) cpu.SetFlag(flag);
			else cpu.RemoveFlag(flag);
		}

		protected void Fetch() => fetched = addressMode.Fetch();

		protected void WriteToStack(byte data)
		{
			cpu.Write((ushort)(0x0100 + cpu.StackPointer), data);

			cpu.StackPointer--;
		}
	}
}