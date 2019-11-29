using FatCat.Nes.OpCodes.IO;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.IO
{
	[UsedImplicitly]
	public class StoreAccumulatorAtAddressTests : StoreItemAtAddressTests
	{
		protected override string ExpectedName => "STA";

		protected override byte StoreData => Accumulator;

		public StoreAccumulatorAtAddressTests() => opCode = new StoreAccumulatorAtAddress(cpu, addressMode);

		protected override void SetDataOnCpu() => cpu.Accumulator = Accumulator;
	}
}