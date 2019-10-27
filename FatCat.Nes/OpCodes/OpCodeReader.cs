using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FatCat.Nes.OpCodes
{
	public class OpCodeReader
	{
		private static List<OpCode> OpCodes { get; } = LoadOpCodes();

		public OpCode Get(byte value) => OpCodes.FirstOrDefault(i => i.Value == value);

		public List<OpCode> GetAll() => OpCodes;

		private static string GetOpCodeJson()
		{
			var nesAssembly = typeof(OpCodeReader).Assembly;

			using var stream = nesAssembly.GetManifestResourceStream("FatCat.Nes.OpCodes.OpCodes.json");
			using var reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}

		private static List<OpCode> LoadOpCodes()
		{
			var opCodeJson = GetOpCodeJson();

			return JsonSerializer.Deserialize<List<OpCode>>(opCodeJson);
		}
	}
}