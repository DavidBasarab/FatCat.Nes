using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace FatCat.Nes.Tests
{
	public class HookUp
	{
		private readonly ITestOutputHelper outputHelper;

		public HookUp(ITestOutputHelper outputHelper) { this.outputHelper = outputHelper; }
		
		[Fact]
		public void SimpleFailing()
		{
			outputHelper.WriteLine("Can I write this!!!!!!!!!!!");
			
			// TestContext.Out.WriteLine("Can I write something in tests?");
			//
			Console.WriteLine("Can I write something in the tests?");
			
			var result = 1 + 3;

			result.Should().Be(3);
		}

		[Fact]
		public void SimpleTest()
		{
			var result = 1 + 1;

			result.Should().Be(2);
		}
	}
}