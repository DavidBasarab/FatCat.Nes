using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Loading
{
	public abstract class LoadingValue : OpCode
	{
		protected LoadingValue(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			SetFetchedValue();

			ApplyFlag(CpuFlag.Zero, fetched.IsZero());
			ApplyFlag(CpuFlag.Negative, fetched.IsNegative());

			return 1;
		}

		protected abstract void SetFetchedValue();
	}
}