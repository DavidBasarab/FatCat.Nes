using FatCat.Nes.OpCodes.IO;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	[UsedImplicitly]
	public class StoreXRegisterAtAddressTests : StoreItemAtAddressTests
	{
		private const byte XRegister = 0x92;

		protected override string ExpectedName => "STX";

		protected override byte StoreData => cpu.XRegister;

		public StoreXRegisterAtAddressTests() => opCode = new StoreXRegisterAtAddress(cpu, addressMode);

		protected override void SetDataOnCpu() => cpu.XRegister = XRegister;
	}
}