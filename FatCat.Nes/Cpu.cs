namespace FatCat.Nes
{
	public class Cpu
	{
		private readonly IBus bus;

		private CpuFlag flag;

		/// <summary>
		///  All used memory addresses end up in here
		/// </summary>
		public ushort AbsoluteAddress { get; set; }

		/// <summary>
		///  A global accumulation of the number of clocks
		/// </summary>
		public int ClockCount { get; set; }

		/// <summary>
		///  Counts how many cycles the instruction has remaining
		/// </summary>
		public int Cycles { get; set; }

		/// <summary>
		///  Represents the working input value to the ALU
		/// </summary>
		public byte Fetched { get; set; }

		/// <summary>
		///  Is the instruction byte
		/// </summary>
		public byte OpCode { get; set; }

		/// <summary>
		///  Represents absolute address following a branch
		/// </summary>
		public ushort RelativeAddress { get; set; }

		public Cpu(IBus bus) => this.bus = bus ?? new Bus();

		public bool GetFlag(CpuFlag cpuFlag) => flag.HasFlag(cpuFlag);

		public byte Read(ushort address) => bus.Read(address);

		public void RemoveFlag(CpuFlag cpuFlag) => flag &= ~cpuFlag;

		public void SetFlag(CpuFlag cpuFlag) => flag |= cpuFlag;

		public void Write(ushort address, byte data) => bus.Write(address, data);
	}
}