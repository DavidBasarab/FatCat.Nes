using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class IndirectModeTests : AddressModeTests
	{
		private const byte HighAddressValue = 0x73;
		private const byte HighPointer = 0xe1;

		private const byte LowAddressValue = 0x14;
		private const byte LowBoundaryPointer = 0xff;
		private const byte LowPointer = 0x43;

		private static ushort BoundaryPointer => (HighPointer << 8) | LowBoundaryPointer;

		private static ushort Pointer => (HighPointer << 8) | LowPointer;

		protected override int ExpectedCycles => 0;

		protected override string ExpectedName => "Indirect";

		public IndirectModeTests()
		{
			addressMode = new IndirectMode(cpu);

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(LowPointer);
			A.CallTo(() => cpu.Read(ProgramCounter + 1)).Returns(HighPointer);

			A.CallTo(() => cpu.Read(Pointer)).Returns(LowAddressValue);
			A.CallTo(() => cpu.Read((ushort)(Pointer + 1))).Returns(HighAddressValue);
		}

		[Fact]
		public void IfPageHardwareBoundaryBugReadHighAddress()
		{
			SetUpBoundaryBug();

			addressMode.Run();

			var expectedHighPointer = BoundaryPointer & 0xff00;

			A.CallTo(() => cpu.Read((ushort)expectedHighPointer)).MustHaveHappened();

			A.CallTo(() => cpu.Read((ushort)(Pointer + 1))).MustNotHaveHappened();
		}

		[Fact]
		public void IfPageHardwareBoundaryBugReadLowAddress()
		{
			SetUpBoundaryBug();

			addressMode.Run();

			A.CallTo(() => cpu.Read(BoundaryPointer)).MustHaveHappened();
		}

		[Fact]
		public void WillReadFromThePointerValueForHighAddress()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read((ushort)(Pointer + 1))).MustHaveHappened();
		}

		[Fact]
		public void WillReadFromThePointerValueForLowAddress()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(Pointer)).MustHaveHappened();
		}

		[Fact]
		public void WillReadHighPointer()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter + 1)).MustHaveHappened();
		}

		[Fact]
		public void WillReadLowPointer()
		{
			addressMode.Run();

			A.CallTo(() => cpu.Read(ProgramCounter)).MustHaveHappened();
		}

		[Fact]
		public void WillSetAbsoluteAddressToReadPointValues()
		{
			addressMode.Run();

			cpu.AbsoluteAddress.Should().Be(0x7314);
		}

		private void SetUpBoundaryBug() => A.CallTo(() => cpu.Read(ProgramCounter)).Returns(LowBoundaryPointer);
	}
}