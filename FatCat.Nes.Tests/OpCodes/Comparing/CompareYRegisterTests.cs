using FatCat.Nes.OpCodes.Comparing;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Comparing
{
	[UsedImplicitly]
	public class CompareYRegisterTests : CompareRegisterTests
	{
		protected override string ExpectedName => "CPY";

		public CompareYRegisterTests() => opCode = new CompareYRegister(cpu, addressMode);

		protected override void SetRegister(byte registerValue) => cpu.YRegister = registerValue;
	}
}