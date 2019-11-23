using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfCarrySet : OpCode
	{
		public override string Name { get; }

		public BranchIfCarrySet(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute() => -1;
	}
}