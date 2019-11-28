using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class RotateRight : OpCode
	{
		public override string Name => "ROR";

		public RotateRight(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => throw new NotImplementedException();
	}
}