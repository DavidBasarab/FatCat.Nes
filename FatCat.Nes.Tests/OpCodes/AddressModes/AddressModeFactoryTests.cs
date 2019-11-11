using System;
using System.Collections.Generic;
using FatCat.Nes.OpCodes.AddressingModes;
using FluentAssertions;
using Xunit;

namespace FatCat.Nes.Tests.OpCodes.AddressModes
{
	public class AddressModeFactoryTests
	{
		public static IEnumerable<object[]> AddressModeData
		{
			get
			{
				yield return new object[] { "Absolute", typeof(AbsoluteMode) };

				yield return new object[] { "Absolute,X", typeof(AbsoluteModeXOffset) };

				yield return new object[] { "Absolute,Y", typeof(AbsoluteModeYOffset) };

				yield return new object[] { "Immediate", typeof(ImmediateMode) };

				yield return new object[] { "Implied", typeof(ImpliedAddressMode) };

				yield return new object[] { "Indirect", typeof(IndirectMode) };

				yield return new object[] { "(Indirect,X)", typeof(IndirectXMode) };

				yield return new object[] { "(Indirect),Y", typeof(IndirectYMode) };

				yield return new object[] { "Relative", typeof(RelativeMode) };

				yield return new object[] { "ZeroPage", typeof(ZeroPageMode) };

				yield return new object[] { "ZeroPage,X", typeof(ZeroPageXOffsetMode) };

				yield return new object[] { "ZeroPage,Y", typeof(ZeroPageYOffsetMode) };
			}
		}

		private readonly AddressModeFactory addressModeFactory;

		public AddressModeFactoryTests() => addressModeFactory = new AddressModeFactory();

		[Theory]
		[MemberData(nameof(AddressModeData), MemberType = typeof(AddressModeFactoryTests))]
		public void WillReturnTheCorrectAddressMode(string name, Type expectedType)
		{
			var addressMode = addressModeFactory.Create(name);

			addressMode.Should().NotBeNull();

			addressMode.Should().BeOfType(expectedType);
		}
	}
}