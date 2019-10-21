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
			
			var random = new Random();

			for (var i = 0; i < 12; i++)
			{
				var address = random.Next(0XFFFF);
				
				PrintAddress(address);
			}

			//PlayingWithBitShifting();

			//WaitForExit();
		}

		private static void OnCancel(object sender, ConsoleCancelEventArgs e)
		{
			stopEvent.Set();

			e.Cancel = true;
		}

		private static void PlayingWithBitShifting()
		{
			Log.Info("Going to Play around with some concepts that I will need for the 6502");

			var someAddress = 0x0103;

			PrintAddress(someAddress);

			Log.Info("Left Shift");
			PrintAddress(someAddress << 2);

			Log.Info("Right Shift");
			PrintAddress(someAddress >> 2);
		}

		private static void PrintAddress(int address)
		{
			IntPtr temp = IntPtr.Zero;
			
			var highAddress = address & 0xFF00;
			var lowAddress = address & 0x00FF;
			
			Log.Info(new string('-', 150));
			
			Log.Info($"Int := {address}  | Hex := {address:x8} | Binary := {Convert.ToString(address, 2)}");
			Log.Info($"    ==> HighAddress | Int := {highAddress}  | Hex := {highAddress:x8} | Binary := {Convert.ToString(highAddress, 2)}");
			Log.Info($"    ==> LowAddress  | Int := {lowAddress}  | Hex := {lowAddress:x8} | Binary := {Convert.ToString(lowAddress, 2)}");
		}

		private static void WaitForExit()
		{
			Log.Debug("Press Control-C to exit . . .");

			stopEvent.WaitOne(-1);

			Log.Warn("Exiting . . . . .");
		}
	}
}