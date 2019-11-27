using FatCat.Nes.OpCodes.IO;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PopStatusOffStackTests : OpCodeTest
	{
		protected override string ExpectedName => "PLP";

		public PopStatusOffStackTests() { opCode = new PopStatusOffStack(cpu, addressMode); }
	}
}