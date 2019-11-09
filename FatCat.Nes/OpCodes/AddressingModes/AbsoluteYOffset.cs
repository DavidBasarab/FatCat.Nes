namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AbsoluteYOffset : Absolute
	{
		public override string Name => "Absolute,Y";

		public AbsoluteYOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			base.Run();

			cpu.AbsoluteAddress += cpu.YRegister;

			return Paged() ? 1 : 0;
		}
	}
}