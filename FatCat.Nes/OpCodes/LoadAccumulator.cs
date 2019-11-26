using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class LoadAccumulator : OpCode
	{
		public override string Name => "LDA";

		public LoadAccumulator(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => throw new NotImplementedException();
	}
}