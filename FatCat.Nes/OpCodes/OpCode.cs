using System.Text.Json.Serialization;

namespace FatCat.Nes.OpCodes
{
	public class OpCode
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
		public string Value { get; set; }
	}
}