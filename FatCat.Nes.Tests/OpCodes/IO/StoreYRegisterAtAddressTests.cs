using FatCat.Nes.OpCodes.IO;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	[UsedImplicitly]
	public class StoreYRegisterAtAddressTests : StoreItemAtAddressTests
	{
		protected override string ExpectedName => "STY";

		protected override byte StoreData => 0x65;

		public StoreYRegisterAtAddressTests() => opCode = new StoreYRegisterAtAddress(cpu, addressMode);

		protected override void SetDataOnCpu() => cpu.YRegister = StoreData;
	}
}