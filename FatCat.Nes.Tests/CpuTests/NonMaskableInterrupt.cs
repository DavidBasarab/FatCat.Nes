using JetBrains.Annotations;

namespace FatCat.Nes.Tests.CpuTests
{
	[UsedImplicitly]
	public class NonMaskableInterrupt : InterruptTests
	{
		protected override int InterruptCycles => 8;

		protected override ushort LowCounterLocation => 0xfffa;

		public NonMaskableInterrupt() => cpu.Nmi();
	}
}