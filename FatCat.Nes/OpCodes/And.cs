using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class And : OpCode
	{
		public And(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override string Name => "AND";

		public override int Execute() => throw new System.NotImplementedException();
	}
}