using System.IO;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests
{
	public class OpCodeReaderTests
	{
		[Fact]
		public void WillBeEmbeddedInTheNesDll()
		{
			var nesAssembly = typeof(Bus).Assembly;

			using var stream = nesAssembly.GetManifestResourceStream("FatCat.Nes.OpCodes.json");
			using var reader = new StreamReader(stream);

			var embeddedJson = reader.ReadToEnd();

			embeddedJson.Should().NotBeEmpty();
		}
	}
}