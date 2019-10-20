using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 4014

namespace FatCat.OneOff
{
	public static class Program
	{
		private static readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

		public static async Task Main(string[] args)
		{
			Console.CancelKeyPress += OnCancel;

			WaitForExit();
		}

		private static void OnCancel(object sender, ConsoleCancelEventArgs e) { stopEvent.Set(); }

		private static void WaitForExit()
		{
			Console.WriteLine("Press Control-C to exit . . .");

			while (!stopEvent.WaitOne(10)) { }

			Console.WriteLine("Exiting . . . .");
		}
	}
}