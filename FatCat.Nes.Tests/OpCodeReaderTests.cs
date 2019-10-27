using System.Collections.Generic;
using System.IO;
using System.Text.Json;
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
									Value = 0x6D
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
									Value = 0x0D
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
									Value = 0x0E
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
									Value = 0xB8
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
									Value = 0x39
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
									Value = 0x90
								}
							};
			}
		}

		private readonly OpCodeReader opCodeReader;

		public OpCodeReaderTests() => opCodeReader = new OpCodeReader();

		[Fact]
		public void CanConvertFromJsonToOpCode()
		{
			var json = @"{
			""cycles"" : ""2(+1ifbranchsucceeds+2iftoanewpage)"",
			""opcode"" : ""$B0"",
			""mode"" : ""Relative"",
			""name"" : ""BCS"",
			""bytes"" : ""2"",
			""description"" : ""Branch if Carry Set""
			}";

			var opCode = JsonSerializer.Deserialize<OpCode>(json);

			opCode.Value.Should().Be(0xB0);
			opCode.Cycles.Should().Be(2);
			opCode.Mode.Should().Be("Relative");
			opCode.Name.Should().Be("BCS");
			opCode.Bytes.Should().Be(2);
			opCode.Description.Should().Be("Branch if Carry Set");
		}

		[Theory]
		[MemberData(nameof(TestOpCodes), MemberType = typeof(OpCodeReaderTests))]
		public void CanReturnOpCodeByAddress(OpCode opCode)
		{
			var returnedOpCode = opCodeReader.Get(opCode.Value);

			returnedOpCode.Should().BeEquivalentTo(opCode);
		}

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