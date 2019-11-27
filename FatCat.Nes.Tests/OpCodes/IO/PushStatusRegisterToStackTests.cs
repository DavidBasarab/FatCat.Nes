using FatCat.Nes.OpCodes.IO;
using FatCat.Nes.Tests.OpCodes.Repository;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	public class PushStatusRegisterToStackTests : OpCodeTest
	{
		protected override string ExpectedName => "PHP";

		public PushStatusRegisterToStackTests() { opCode = new PushStatusRegisterToStack(cpu, addressMode); }
	}
}