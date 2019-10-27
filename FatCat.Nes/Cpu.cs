namespace FatCat.Nes
{
	public class Cpu
	{
		private readonly IBus bus;

		/// <summary>
		///  All used memory addresses end up in here
		/// </summary>
		public ushort AbsoluteAddress { get; set; }

		/// <summary>
		///  Accumulator Register
		/// </summary>
		public byte Accumulator { get; set; }

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
		///  Program Counter
		/// </summary>
		public ushort ProgramCounter { get; set; }

		/// <summary>
		///  Represents absolute address following a branch
		/// </summary>
		public ushort RelativeAddress { get; set; }

		/// <summary>
		///  Stack Pointer (points to location on bus)
		/// </summary>
		public byte StackPointer { get; set; }

		/// <summary>
		///  Status Register
		/// </summary>
		public CpuFlag StatusRegister { get; set; }

		/// <summary>
		///  X Register
		/// </summary>
		public byte XRegister { get; set; }

		/// <summary>
		///  Y Register
		/// </summary>
		public byte YRegister { get; set; }

		public Cpu(IBus bus) => this.bus = bus ?? new Bus();

		public bool GetFlag(CpuFlag cpuFlag) => StatusRegister.HasFlag(cpuFlag);

		public byte Read(ushort address) => bus.Read(address);

		public void RemoveFlag(CpuFlag cpuFlag) => StatusRegister &= ~cpuFlag;

		public void SetFlag(CpuFlag cpuFlag) => StatusRegister |= cpuFlag;

		public void Write(ushort address, byte data) => bus.Write(address, data);
	}
}