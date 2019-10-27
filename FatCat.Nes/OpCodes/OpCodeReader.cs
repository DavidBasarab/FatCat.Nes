using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FatCat.Nes.OpCodes
{
	public class OpCodeReader
	{
		public List<OpCode> GetAll()
		{
			var opCodeJson = GetOpCodeJson();

			return JsonSerializer.Deserialize<List<OpCode>>(opCodeJson);
		}

		private string GetOpCodeJson()
		{
			var nesAssembly = typeof(OpCodeReader).Assembly;

			using var stream = nesAssembly.GetManifestResourceStream("FatCat.Nes.OpCodes.json");
			using var reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
	}
}