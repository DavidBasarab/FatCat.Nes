namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AbsoluteModeYOffset : AbsoluteMode
	{
		public override string Name => "Absolute,Y";

		public AbsoluteModeYOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			base.Run();

			cpu.AbsoluteAddress += cpu.YRegister;

			return Paged() ? 1 : 0;
		}
	}
}