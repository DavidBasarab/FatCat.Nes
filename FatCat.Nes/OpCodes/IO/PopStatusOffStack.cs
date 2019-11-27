using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.IO
{
	public class PopStatusOffStack : OpCode
	{
		public override string Name => "PLP";

		public PopStatusOffStack(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var temp = ReadFromStack();
			
			return -1;
		}
	}
}