using FatCat.Nes.OpCodes.Arithmetic;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class RotateRightTests : OpCodeTest
	{
		protected override string ExpectedName => "ROR";

		public RotateRightTests() { opCode = new RotateRight(cpu, addressMode); }
	}
}