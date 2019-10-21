using System;
using System.Threading;
using System.Threading.Tasks;

namespace FatCat.OneOff
{
	public static class Program
	{
		private static readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

		public static async Task Main(string[] args)
		{
			Console.CancelKeyPress += OnCancel;

			WaitForExit();

			await Task.Delay(100);
			
			Console.WriteLine("After Stuff");
		}

		private static void OnCancel(object sender, ConsoleCancelEventArgs e)
		{
			Console.WriteLine("Got Cancel Event");
			
			stopEvent.Set();

			e.Cancel = true;
		}

		private static void WaitForExit()
		{
			Console.WriteLine("Press Control-C to exit . . .");

			stopEvent.WaitOne(-1);

			Console.WriteLine("Exiting . . . .");
		}
	}
}