using System;
using System.Text.Json.Serialization;

namespace FatCat.Nes
{
	public class OpCode
	{
		//[JsonPropertyName("bytes")]
		public int Bytes { get; set; }

		//[JsonPropertyName("cycles")]
		public int Cycles { get; set; }

		//[JsonPropertyName("description")]
		public string Description { get; set; }

		//[JsonPropertyName("mode")]
		public string Mode { get; set; }

		//[JsonPropertyName("name")]
		public string Name { get; set; }

		//[JsonPropertyName("opcode")]
		public string Value { get; set; }
	}
}