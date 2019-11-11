using System;
using System.Collections.Generic;
using System.Linq;
using FatCat.Nes.Infrastructure;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class AddressModeFactory
	{
		private static List<AddressMode> addressModes;

		private static List<AddressMode> AddressModes
		{
			get { return addressModes ??= CreateAddressModes(); }
		}

		public AddressMode Create(string name) => AddressModes.FirstOrDefault(i => i.Name == name);

		private static List<AddressMode> CreateAddressModes()
		{
			var addressModeType = typeof(AddressMode);

			var addressModeItems = AppDomain.CurrentDomain.GetAssemblies()
											.SelectMany(i => i.GetTypes())
											.Where(i => addressModeType.IsAssignableFrom(i) && !i.IsAbstract)
											.ToList();

			return addressModeItems.Select(i => Injector.Get<AddressMode>(i)).ToList();
		}
	}
}