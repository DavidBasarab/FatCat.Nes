using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using JetBrains.Annotations;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class DecrementValueAtMemoryTests : OpCodeTest
	{
		private const int AbsoluteAddress = 0x5218;

		public static IEnumerable<object[]> NonZeroData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x02 // fetched
							};

				yield return new object[]
							{
								0xff // fetched
							};

				yield return new object[]
							{
								0x00 // fetched
							};
			}
		}

		public static IEnumerable<object[]> ZeroData
		{
			[UsedImplicitly]
			get
			{
				yield return new object[]
							{
								0x01 // fetched
							};
			}
		}

		protected override string ExpectedName => "DEC";

		public DecrementValueAtMemoryTests()
		{
			opCode = new DecrementValueAtMemory(cpu, addressMode);

			cpu.AbsoluteAddress = AbsoluteAddress;
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}

		[Theory]
		[MemberData(nameof(ZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillSetTheZeroFlag(byte fetched)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustHaveHappened();
			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustNotHaveHappened();
		}

		[Theory]
		[MemberData(nameof(NonZeroData), MemberType = typeof(DecrementValueAtMemoryTests))]
		public void WillUnsetTheZeroFlag(byte fetched)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
			A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustNotHaveHappened();
		}

		[Fact]
		public void WillWriteTheIncrementDataToMemory()
		{
			opCode.Execute();

			byte expectedWriteData = (FetchedData - 1) & 0x00ff;

			A.CallTo(() => cpu.Write(AbsoluteAddress, expectedWriteData)).MustHaveHappened();
		}
	}
}