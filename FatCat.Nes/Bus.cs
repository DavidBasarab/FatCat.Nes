using System.Collections.Generic;

namespace FatCat.Nes
{
	public class Bus
	{
		internal int[] Ram { get; private set; } = new int[64 * 1024];
	}
}