using FatCat.Nes.OpCodes.Comparing;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Comparing
{
	[UsedImplicitly]
	public class CompareXRegisterTests : CompareRegisterTests
	{
		protected override string ExpectedName => "CPX";

		public CompareXRegisterTests() => opCode = new CompareXRegister(cpu, addressMode);

		protected override void SetRegister(byte registerValue) => cpu.XRegister = registerValue;
	}
}