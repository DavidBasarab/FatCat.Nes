using System;
using System.Text.Json.Serialization;

namespace FatCat.Nes.OpCodes
{
	// TODO remove after all op codes implemented
	public class TestingOpCode : OpCode
	{
		// This will need to rethink the reader
		public TestingOpCode()
			: base(null) { }

		public TestingOpCode(ICpu cpu) : base(cpu) { }

		public override int Execute() => throw new NotImplementedException();
	}

	public abstract class OpCode
	{
		protected readonly ICpu cpu;

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

		protected OpCode(ICpu cpu) => this.cpu = cpu;

		public abstract int Execute();
	}
}