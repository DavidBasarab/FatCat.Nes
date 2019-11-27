using FakeItEasy;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public abstract class AddressModeTests
	{
		protected const ushort ProgramCounter = 0xef2;
		protected const byte ReadValue = 0x38;

		protected readonly ICpu cpu;

		protected AddressMode addressMode;

		protected abstract int ExpectedCycles { get; }

		protected abstract string ExpectedName { get; }

		protected AddressModeTests()
		{
			cpu = A.Fake<ICpu>();

			cpu.ProgramCounter = ProgramCounter;

			A.CallTo(() => cpu.Read(ProgramCounter)).Returns(ReadValue);
		}

		[Fact]
		public void OnFetchWillReadFromAbsoluteAddress()
		{
			ushort absoluteAddress = 0x1970;

			cpu.AbsoluteAddress = absoluteAddress;

			addressMode.Fetch();

			VerifyReadFromAbsoluteAddress(absoluteAddress);
		}

		[Fact]
		public void OnFetchWillReturnFetchedValue()
		{
			ushort absoluteAddress = 0x1944;

			cpu.AbsoluteAddress = absoluteAddress;

			byte fetchedValue = 0x52;
			byte startingFetchedValue = 0xd1;

			cpu.Fetched = startingFetchedValue;

			A.CallTo(() => cpu.Read(absoluteAddress)).Returns(fetchedValue);

			var fetchResult = addressMode.Fetch();

			VerifyFetchResult(fetchResult, fetchedValue, startingFetchedValue);
		}

		[Fact]
		public void OnFetchWillSetTheCpuFetchValue()
		{
			ushort absoluteAddress = 0x1944;

			cpu.AbsoluteAddress = absoluteAddress;

			byte fetchedValue = 0x52;
			byte startingFetchedValue = 0xd1;

			cpu.Fetched = startingFetchedValue;

			A.CallTo(() => cpu.Read(absoluteAddress)).Returns(fetchedValue);

			addressMode.Fetch();

			VerifyFetchedValue(fetchedValue, startingFetchedValue);
		}

		[Fact]
		public void RunWillTakeCycles()
		{
			var cycles = addressMode.Run();

			cycles.Should().Be(ExpectedCycles);
		}

		[Fact]
		public void WillHaveCorrectAddressModeName() => addressMode.Name.Should().Be(ExpectedName);

		protected virtual void VerifyFetchedValue(byte fetchedValue, byte startingFetchedValue) => cpu.Fetched.Should().Be(fetchedValue);

		protected virtual void VerifyFetchResult(byte fetchResult, byte fetchedValue, byte startingFetchedValue) => fetchResult.Should().Be(fetchedValue);

		protected virtual void VerifyReadFromAbsoluteAddress(ushort absoluteAddress) => A.CallTo(() => cpu.Read(absoluteAddress)).MustHaveHappened();
	}
}