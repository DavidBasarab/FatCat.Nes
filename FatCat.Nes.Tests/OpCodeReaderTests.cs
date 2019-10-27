using System.Collections.Generic;
using System.IO;
using FatCat.Nes.OpCodes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests
{
	public class OpCodeReaderTests
	{
		public static IEnumerable<object[]> TestOpCodes
		{
			get
			{
				yield return new object[]
							{
								new OpCode
								{
									Bytes = 3,
									Cycles = 4,
									Description = "Add with Carry",
									Mode = "Absolute",
									Name = "ADC",
									Value = "$6D"
								}
							};

				yield return new object[]
							{
								new OpCode
								{
									Bytes = 3,
									Cycles = 4,
									Description = "Logical Inclusive OR",
									Mode = "Absolute",
									Name = "ORA",
									Value = "$0D"
								}
							};

				yield return new object[]
							{
								new OpCode
								{
									Bytes = 3,
									Cycles = 6,
									Description = "Arithmetic Shift Left",
									Mode = "Absolute",
									Name = "ASL",
									Value = "$0E"
								}
							};

				yield return new object[]
							{
								new OpCode
								{
									Bytes = 1,
									Cycles = 2,
									Description = "Clear Overflow Flag",
									Mode = "Implied",
									Name = "CLV",
									Value = "$B8"
								}
							};

				yield return new object[]
							{
								new OpCode
								{
									Bytes = 3,
									Cycles = 4,
									Description = "Logical AND",
									Mode = "Absolute,Y",
									Name = "AND",
									Value = "$39"
								}
							};

				yield return new object[]
							{
								new OpCode
								{
									Bytes = 2,
									Cycles = 2,
									Description = "Branch if Carry Clear",
									Mode = "Relative",
									Name = "BCC",
									Value = "$90"
								}
							};
			}
		}

		private readonly OpCodeReader opCodeReader;

		public OpCodeReaderTests() => opCodeReader = new OpCodeReader();

		[Fact]
		public void WillBeEmbeddedInTheNesDll()
		{
			var nesAssembly = typeof(OpCodeReader).Assembly;

			using var stream = nesAssembly.GetManifestResourceStream("FatCat.Nes.OpCodes.OpCodes.json");
			using var reader = new StreamReader(stream);

			var embeddedJson = reader.ReadToEnd();

			embeddedJson.Should().NotBeEmpty();
		}

		[Theory]
		[MemberData(nameof(TestOpCodes), MemberType = typeof(OpCodeReaderTests))]
		public void WillConvertToOpCodeCorrectly(OpCode expectedOpCode)
		{
			var opCodes = opCodeReader.GetAll();

			opCodes.Should().ContainEquivalentOf(expectedOpCode);
		}

		[Fact]
		public void WillReturn150OpCodes()
		{
			var opCodes = opCodeReader.GetAll();

			opCodes.Count.Should().Be(151);
		}
	}
}