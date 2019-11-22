using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FatCat.Nes.OpCodes.Repository
{
	public class JsonConverterOpCodeByteCode : JsonConverter<byte>
	{
		public override byte Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

				if (Utf8Parser.TryParse(span, out byte number, out var bytesConsumed) && span.Length == bytesConsumed) return number;

				var jsonValue = reader.GetString();

				jsonValue = jsonValue.Replace("$", string.Empty);

				var value = Convert.ToByte(jsonValue, 16);

				return value;
			}

			return reader.GetByte();
		}

		public override void Write(Utf8JsonWriter writer, byte value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
	}
}