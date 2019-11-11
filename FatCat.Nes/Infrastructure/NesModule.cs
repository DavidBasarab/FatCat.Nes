using Ninject.Modules;

namespace FatCat.Nes.Infrastructure
{
	public class NesModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICpu>().To<Cpu>();
			Bind<IBus>().To<Bus>();
		}
	}
}