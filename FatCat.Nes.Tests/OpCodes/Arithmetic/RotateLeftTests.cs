using FakeItEasy;
using FatCat.Nes.OpCodes.Arithmetic;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.Arithmetic
{
	public class RotateLeftTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0xe1c1;

		protected override string ExpectedName => "ROL";

		public RotateLeftTests()
		{
			opCode = new RotateLeft(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void IfAddressModeIsImpliedThenValueIsWrittenToAccumulator()
		{
			A.CallTo(() => addressMode.Name).Returns("Implied");

			opCode.Execute();

			byte valueToWrite = (FetchedData << 1) & 0x00ff;

			cpu.Accumulator.Should().Be(valueToWrite);

			A.CallTo(() => cpu.Write(A<ushort>.Ignored, A<byte>.Ignored)).MustNotHaveHappened();
		}

		[Fact]
		public void IfCarryFlagIsSetWillOrWithCarryFlag()
		{
			A.CallTo(() => cpu.GetFlag(CpuFlag.CarryBit)).Returns(true);

			opCode.Execute();

			byte valueToWrite = ((FetchedData << 1) | 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, valueToWrite)).MustHaveHappened();

			cpu.Accumulator.Should().Be(Accumulator);
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Fact]
		public void WillWriteTheRightShiftedValue()
		{
			opCode.Execute();

			byte valueToWrite = (FetchedData << 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, valueToWrite)).MustHaveHappened();

			cpu.Accumulator.Should().Be(Accumulator);
		}
	}
}