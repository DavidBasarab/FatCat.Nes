using System;
using Ninject;

namespace FatCat.Nes.Infrastructure
{
	public static class Injector
	{
		private static readonly StandardKernel standardKernel = new StandardKernel(new NesModule());

		public static T Get<T>() => standardKernel.Get<T>();

		public static T Get<T>(Type type) => (T)standardKernel.Get(type);
	}
}