using System;
using System.Text.Json.Serialization;

namespace FatCat.Nes.OpCodes
{
	public class AddWithCarry : OpCode
	{
		public override int Execute() => throw new NotImplementedException();
	}

	// TODO remove after all op codes implemented
	public class TestingOpCode : OpCode
	{
		public override int Execute() => throw new NotImplementedException();
	}

	public abstract class OpCode
	{
		[JsonPropertyName("bytes")]
		[JsonConverter(typeof(JsonConverterStringToInt))]
		public int Bytes { get; set; }

		[JsonPropertyName("cycles")]
		[JsonConverter(typeof(JsonConverterOpCodeCycle))]
		public int Cycles { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("mode")]
		public string Mode { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("opcode")]
		[JsonConverter(typeof(JsonConverterOpCodeByteCode))]
		public byte Value { get; set; }

		public abstract int Execute();
	}
}