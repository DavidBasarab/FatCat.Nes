using System;
using System.Threading;

namespace FatCat.OneOff
{
	public static class Program
	{
		private static readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

		public static void Main(string[] args)
		{
			Console.CancelKeyPress += OnCancel;

			//WaitForExit();
		}

		private static void OnCancel(object sender, ConsoleCancelEventArgs e)
		{
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