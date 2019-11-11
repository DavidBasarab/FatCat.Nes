using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace FatCat.Nes.OpCodes.AddressingModes
{
	public class NesModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICpu>().To<Cpu>();
			Bind<IBus>().To<Bus>();
		}
	}

	public static class AddressModeFactory
	{
		private static List<AddressMode> addressModes;

		private static List<AddressMode> AddressModes
		{
			get
			{
				if (addressModes == null)
				{
					var addressModeType = typeof(AddressMode);

					var addressModeItems = AppDomain.CurrentDomain.GetAssemblies()
													.SelectMany(i => i.GetTypes())
													.Where(i => addressModeType.IsAssignableFrom(i) && !i.IsAbstract)
													.ToList();

					addressModes = new List<AddressMode>();

					var kernel = new StandardKernel(new NesModule());

					foreach (var addressModeItem in addressModeItems) addressModes.Add(kernel.Get(addressModeItem) as AddressMode);
				}

				return addressModes;
			}
		}

		public static AddressMode Create(string name) => AddressModes.FirstOrDefault(i => i.Name == name);
	}
}