using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.Loading;
using FluentAssertions;
using JetBrains.Annotations;

namespace FatCat.Nes.Tests.OpCodes.Loading
{
	[UsedImplicitly]
	public class LoadXRegisterTests : LoadingTests
	{
		protected override string ExpectedName => "LDX";

		public LoadXRegisterTests() => opCode = new LoadXRegister(cpu, addressMode);

		protected override void VerifyCpuValueSet() => cpu.XRegister.Should().Be(FetchedData);
	}
}