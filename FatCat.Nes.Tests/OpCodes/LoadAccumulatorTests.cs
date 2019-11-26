using System.Collections.Generic;
using FakeItEasy;
using FatCat.Nes.OpCodes;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes
{
	public class LoadAccumulatorTests : OpCodeTest
	{
		public static IEnumerable<object[]> ZeroFlagData
		{
			get
			{
				yield return new object[]
							{
								0x00, // fetched
								true  // flag set
							};

				yield return new object[]
							{
								0x01, // fetched
								false // flag set
							};

				yield return new object[]
							{
								0xff, // fetched
								false // flag set
							};
			}
		}

		protected override string ExpectedName => "LDA";

		public LoadAccumulatorTests() => opCode = new LoadAccumulator(cpu, addressMode);

		[Theory]
		[MemberData(nameof(ZeroFlagData), MemberType = typeof(LoadAccumulatorTests))]
		public void ApplyingZeroFlag(byte fetched, bool flagSet)
		{
			A.CallTo(() => addressMode.Fetch()).Returns(fetched);

			opCode.Execute();

			if (flagSet)
			{
				A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustHaveHappened();
				A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustNotHaveHappened();
			}
			else
			{
				A.CallTo(() => cpu.RemoveFlag(CpuFlag.Zero)).MustHaveHappened();
				A.CallTo(() => cpu.SetFlag(CpuFlag.Zero)).MustNotHaveHappened();
			}
		}

		[Fact]
		public void WillFetchFromTheAddressMode()
		{
			opCode.Execute();

			A.CallTo(() => addressMode.Fetch()).MustHaveHappened();
		}
	}
}