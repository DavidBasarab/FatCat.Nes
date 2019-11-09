namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AbsoluteXOffset : Absolute
	{
		public override string Name => "Absolute,X";

		public AbsoluteXOffset(ICpu cpu) : base(cpu) { }

		public override int Run()
		{
			base.Run();

			cpu.AbsoluteAddress += cpu.XRegister;

			return Paged() ? 1 : 0;
		}

		private bool Paged()
		{
			var highAddressValue = cpu.AbsoluteAddress & 0xff00;

			return highAddressValue != high << 8;
		}
	}
}