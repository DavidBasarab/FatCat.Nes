using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class SubtractWithCarry : OpCode
	{
		public override string Name => "SBC";

		public SubtractWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			var fetchedData = addressMode.Fetch();
			
			return -1;
		}
	}
}