using FluentAssertions;
using NUnit.Framework;

namespace FatCat.Nes.Tests
{
	[TestFixture]
	public class HookUp
	{
		[Test]
		public void SimpleFailing()
		{
			var result = 1 + 3;

			result.Should().Be(2);
		}

		[Test]
		public void SimpleTest()
		{
			var result = 1 + 1;

			result.Should().Be(2);
		}
	}
}