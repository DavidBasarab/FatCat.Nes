using FatCat.Nes.OpCodes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class ReturnFromSubRoutineTests : OpCodeTest
	{
		protected override string ExpectedName => "RTS";

		public ReturnFromSubRoutineTests() { opCode = new ReturnFromSubRoutine(cpu, addressMode); }
	}
}