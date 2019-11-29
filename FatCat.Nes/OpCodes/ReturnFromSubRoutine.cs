using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class ReturnFromSubRoutine : OpCode
	{
		public override string Name => "RTS";

		public ReturnFromSubRoutine(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var lowCounter = ReadFromStack();
			var highCounter = ReadFromStack();
			
			return -1;
		}
	}
}