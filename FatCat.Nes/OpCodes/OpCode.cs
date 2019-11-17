namespace FatCat.Nes.OpCodes
{
	public abstract class OpCode
	{
		protected readonly ICpu cpu;

		protected OpCode(ICpu cpu) => this.cpu = cpu;

		public abstract int Execute();
	}
}