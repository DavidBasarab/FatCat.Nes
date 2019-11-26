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

			ApplyFlag(fetched.IsZero(), CpuFlag.Zero);
			ApplyFlag(fetched.IsNegative(), CpuFlag.Negative);

			return 1;
		}

		protected abstract void SetFetchedValue();
	}
}