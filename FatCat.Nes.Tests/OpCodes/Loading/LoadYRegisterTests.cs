using FatCat.Nes.OpCodes.Loading;
using FluentAssertions;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	[UsedImplicitly]
	public class LoadYRegisterTests : LoadingTests
	{
		protected override string ExpectedName => "LDY";

		public LoadYRegisterTests() => opCode = new LoadYRegister(cpu, addressMode);

		protected override void VerifyCpuValueSet() => cpu.YRegister.Should().Be(FetchedData);
	}
}