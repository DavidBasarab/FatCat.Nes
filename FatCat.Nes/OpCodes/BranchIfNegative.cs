using System;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfNegative : BranchOpCode
	{
		public override string Name { get; }

		public BranchIfNegative(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => throw new NotImplementedException();
	}
}