using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class Break : OpCode
	{
		public override string Name => "BRK";

		public Break(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.ProgramCounter++;

			cpu.SetFlag(CpuFlag.DisableInterrupts);

			WriteToStack((byte)((cpu.ProgramCounter >> 8) & 0x00ff));
			WriteToStack((byte)(cpu.ProgramCounter & 0x00ff));

			cpu.SetFlag(CpuFlag.Break);

			WriteToStack((byte)cpu.StatusRegister);

			var lowCounter = cpu.Read(0xffff);
			var highCounter = cpu.Read(0xfffe);

			cpu.ProgramCounter = (ushort)(highCounter << 8 | lowCounter);

			return -1;
		}

		private void WriteToStack(byte data)
		{
			cpu.Write((ushort)(0x0100 + cpu.StackPointer), data);

			cpu.StackPointer--;
		}
	}
}