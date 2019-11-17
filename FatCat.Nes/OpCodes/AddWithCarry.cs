using System;

namespace FatCat.Nes.OpCodes
{
	public class AddWithCarry : OpCode
	{
		public override int Execute() => throw new NotImplementedException();

		public AddWithCarry(ICpu cpu) : base(cpu) { }
	}
}