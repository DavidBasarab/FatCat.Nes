namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AbsoluteModeXOffset : AbsoluteMode
	{
		public override string Name => "Absolute,X";

		public AbsoluteModeXOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			base.Run();

			cpu.AbsoluteAddress += cpu.XRegister;

			return Paged() ? 1 : 0;
		}
	}
}