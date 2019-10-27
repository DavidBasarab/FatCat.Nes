using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FatCat.Nes.OpCodes
{
	public class OpCodeReader
	{
		private List<OpCode> OpCodes { get; set; }
		
		public List<OpCode> GetAll()
		{
			return OpCodes ??= LoadOpCodes();
		}

		private List<OpCode> LoadOpCodes()
		{
			var opCodeJson = GetOpCodeJson();

			return JsonSerializer.Deserialize<List<OpCode>>(opCodeJson);
		}

		private string GetOpCodeJson()
		{
			var nesAssembly = typeof(OpCodeReader).Assembly;

			using var stream = nesAssembly.GetManifestResourceStream("FatCat.Nes.OpCodes.OpCodes.json");
			using var reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
	}
}