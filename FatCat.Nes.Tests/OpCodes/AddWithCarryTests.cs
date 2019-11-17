using FakeItEasy;
using FatCat.Nes.OpCodes;
using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.Tests.OpCodes
{
	public class AddWithCarryTests
	{
		private readonly IAddressMode addressMode;
		private readonly ICpu cpu;
		private AddWithCarry opCode;

		public AddWithCarryTests()
		{
			cpu = A.Fake<ICpu>();
			addressMode = A.Fake<IAddressMode>();

			opCode = new AddWithCarry(cpu, addressMode);
		}
	}
}