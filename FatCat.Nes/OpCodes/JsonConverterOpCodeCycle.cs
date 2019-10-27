using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FatCat.Nes.OpCodes
{
	public class JsonConverterOpCodeCycle : JsonConverter<int>
	{
		public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

				if (Utf8Parser.TryParse(span, out int number, out var bytesConsumed) && span.Length == bytesConsumed) return number;

				var jsonValue = reader.GetString();

				if (int.TryParse(jsonValue[0].ToString(), out number)) return number;
			}

			return reader.GetInt32();
		}

		public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
	}
}