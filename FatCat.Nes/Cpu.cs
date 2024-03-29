namespace FatCat.Nes
{
	public interface ICpu
	{
		/// <summary>
		///  All used memory addresses end up in here
		/// </summary>
		ushort AbsoluteAddress { get; set; }

		/// <summary>
		///  Accumulator Register
		/// </summary>
		byte Accumulator { get; set; }

		/// <summary>
		///  A global accumulation of the number of clocks
		/// </summary>
		int ClockCount { get; set; }

		/// <summary>
		///  Counts how many cycles the instruction has remaining
		/// </summary>
		int Cycles { get; set; }

		/// <summary>
		///  Represents the working input value to the ALU
		/// </summary>
		byte Fetched { get; set; }

		/// <summary>
		///  Is the instruction byte
		/// </summary>
		byte OpCode { get; set; }

		/// <summary>
		///  Program Counter
		/// </summary>
		ushort ProgramCounter { get; set; }

		/// <summary>
		///  Represents absolute address following a branch
		/// </summary>
		ushort RelativeAddress { get; set; }

		/// <summary>
		///  Stack Pointer (points to location on bus)
		/// </summary>
		byte StackPointer { get; set; }

		/// <summary>
		///  Status Register
		/// </summary>
		CpuFlag StatusRegister { get; set; }

		/// <summary>
		///  X Register
		/// </summary>
		byte XRegister { get; set; }

		/// <summary>
		///  Y Register
		/// </summary>
		byte YRegister { get; set; }

		bool GetFlag(CpuFlag cpuFlag);

		/// <summary>
		///  Interrupt requests are a complex operation and only happen if the
		///  "disable interrupt" flag is 0. IRQs can happen at any time, but
		///  you dont want them to be destructive to the operation of the running
		///  program. Therefore the current instruction is allowed to finish
		///  (which I facilitate by doing the whole thing when cycles == 0) and
		///  then the current program counter is stored on the stack. Then the
		///  current status register is stored on the stack. When the routine
		///  that services the interrupt has finished, the status register
		///  and program counter can be restored to how they where before it
		///  occurred. This is implemented by the "RTI" instruction. Once the IRQ
		///  has happened, in a similar way to a reset, a programmable address
		///  is read form hard coded location 0xFFFE, which is subsequently
		///  set to the program counter.
		/// </summary>
		void Irq();

		void Nmi();

		byte Read(ushort address);

		void RemoveFlag(CpuFlag cpuFlag);

		/// <summary>
		///  Forces the 6502 into a known state. This is hard-wired inside the CPU. The
		///  registers are set to 0x00, the status register is cleared except for unused
		///  bit which remains at 1. An absolute address is read from location 0xFFFC
		///  which contains a second address that the program counter is set to. This
		///  allows the programmer to jump to a known and programmable location in the
		///  memory to start executing from. Typically the programmer would set the value
		///  at location 0xFFFC at compile time.
		/// </summary>
		void Reset();

		void SetFlag(CpuFlag cpuFlag);

		void Write(ushort address, byte data);
	}

	public class Cpu : ICpu
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

		/// <summary>
		///  Interrupt requests are a complex operation and only happen if the
		///  "disable interrupt" flag is 0. IRQs can happen at any time, but
		///  you dont want them to be destructive to the operation of the running
		///  program. Therefore the current instruction is allowed to finish
		///  (which I facilitate by doing the whole thing when cycles == 0) and
		///  then the current program counter is stored on the stack. Then the
		///  current status register is stored on the stack. When the routine
		///  that services the interrupt has finished, the status register
		///  and program counter can be restored to how they where before it
		///  occurred. This is implemented by the "RTI" instruction. Once the IRQ
		///  has happened, in a similar way to a reset, a programmable address
		///  is read form hard coded location 0xFFFE, which is subsequently
		///  set to the program counter.
		/// </summary>
		public void Irq()
		{
			if (GetFlag(CpuFlag.DisableInterrupts)) return;

			RunInterrupt(0xfffe, 7);
		}

		public void Nmi() => RunInterrupt(0xfffa, 8);

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
			AbsoluteAddress = 0xfffc;

			SetProgramCounter();

			Accumulator = 0x00;
			XRegister = 0x00;
			YRegister = 0x00;
			StackPointer = 0xfd;
			StatusRegister = CpuFlag.Unused;

			AbsoluteAddress = 0x0000;
			RelativeAddress = 0x0000;
			Fetched = 0x00;

			Cycles = 8;
		}

		public void SetFlag(CpuFlag cpuFlag) => StatusRegister |= cpuFlag;

		public void Write(ushort address, byte data) => bus.Write(address, data);

		private void PushToStack(byte data)
		{
			Write((ushort)(0x0100 + StackPointer), data);

			StackPointer -= 1;
		}

		private void RunInterrupt(ushort programCounterLocation, int cycles)
		{
			PushToStack((ProgramCounter >> 8).ApplyLowMask());
			PushToStack(ProgramCounter.ApplyLowMask());

			RemoveFlag(CpuFlag.Break);
			SetFlag(CpuFlag.Unused);
			SetFlag(CpuFlag.DisableInterrupts);

			PushToStack((byte)StatusRegister);

			AbsoluteAddress = programCounterLocation;

			SetProgramCounter();

			Cycles = cycles;
		}

		private void SetProgramCounter()
		{
			var lowCounter = bus.Read(AbsoluteAddress);
			var highCounter = bus.Read((ushort)(AbsoluteAddress + 1));

			ProgramCounter = (ushort)((highCounter << 8) | lowCounter);
		}
	}
}