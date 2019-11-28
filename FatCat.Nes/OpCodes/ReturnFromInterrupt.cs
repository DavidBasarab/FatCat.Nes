using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ReturnFromInterrupt : OpCode
	{
		public override string Name => "RTI";

		public ReturnFromInterrupt(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			cpu.StatusRegister = (CpuFlag)ReadFromStack();
			
			cpu.RemoveFlag(CpuFlag.Break);
			cpu.RemoveFlag(CpuFlag.Unused);

			var lowCounter = ReadFromStack();
			var highCounter = ReadFromStack();

			return -1;
		}
	}
}