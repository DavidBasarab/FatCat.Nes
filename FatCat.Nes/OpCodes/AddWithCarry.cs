using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class AddWithCarry : OpCode
	{
		public AddWithCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => throw new NotImplementedException();
	}
}