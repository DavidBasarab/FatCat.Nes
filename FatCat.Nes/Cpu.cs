namespace FatCat.Nes
{
	public class Cpu
	{
		private readonly IBus bus;

		/// <summary>
		///  All used memory addresses end up in here
		/// </summary>
		private ushort absoluteAddress;

		/// <summary>
		///  Represents the working input value to the ALU
		/// </summary>
		private byte fetched;

		/// <summary>
		///  Is the instruction byte
		/// </summary>
		private byte opCode;

		/// <summary>
		///  Represents absolute address following a branch
		/// </summary>
		private ushort relativeAddress;

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
		///  Program Counter
		/// </summary>
		public ushort ProgramCounter { get; set; }

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

		/// <summary>
		///  Forces the 6502 into a known state. This is hard-wired inside the CPU. The
		///  registers are set to 0x00, the status register is cleared except for unused
		///  bit which remains at 1. An absolute address is read from location 0xFFFC
		///  which contains a second address that the program counter is set to. This
		///  allows the programmer to jump to a known and programmable location in the
		///  memory to start executing from. Typically the programmer would set the value
		///  at location 0xFFFC at compile time.
		/// </summary>
		public void Reset()
		{
			absoluteAddress = 0xfffc;

			var lowAddress = Read(absoluteAddress);
			var highAddress = Read((ushort)(absoluteAddress + 1));

			ProgramCounter = (ushort)((highAddress << 8) | lowAddress);
		}

		public void SetFlag(CpuFlag cpuFlag) => StatusRegister |= cpuFlag;

		public void Write(ushort address, byte data) => bus.Write(address, data);
	}
}