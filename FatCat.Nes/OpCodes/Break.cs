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

			PushToStack((byte)((cpu.ProgramCounter >> 8) & 0x00ff));
			PushToStack((byte)(cpu.ProgramCounter & 0x00ff));

			cpu.SetFlag(CpuFlag.Break);

			PushToStack((byte)cpu.StatusRegister);

			SetNewProgramCounterLocation();

			return 0;
		}

		private void SetNewProgramCounterLocation()
		{
			var lowCounter = cpu.Read(0xffff);
			var highCounter = cpu.Read(0xfffe);

			cpu.ProgramCounter = (ushort)((highCounter << 8) | lowCounter);
		}
	}
}