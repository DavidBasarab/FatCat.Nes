using System;
using System.Threading;
using FatCat.OneOff.Common;

namespace FatCat.OneOff
{
	public static class Program
	{
		private static readonly ManualResetEvent stopEvent = new ManualResetEvent(false);

		public static void Main(string[] args)
		{
			Console.CancelKeyPress += OnCancel;
			
			Log.Info("Going to Play around with some concepts that I will need for the 6502");

			var someAddress = 0x0103;
			
			PrintAddress(someAddress);
			
			Log.Info("Left Shift");
			PrintAddress(someAddress << 2);
			
			Log.Info("Right Shift");
			PrintAddress(someAddress >> 2);

			//WaitForExit();
		}

		private static void PrintAddress(int address) => Log.Info($"Int := {address}  | Hex := {address:x8} | Binary := {Convert.ToString(address, 2)}");

		private static void OnCancel(object sender, ConsoleCancelEventArgs e)
		{
			stopEvent.Set();

			e.Cancel = true;
		}

		private static void WaitForExit()
		{
			Log.Debug("Press Control-C to exit . . .");

			stopEvent.WaitOne(-1);

			Log.Warn("Exiting . . . . .");
		}
	}
}