using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        private readonly Dictionary<char, Action<IVirtualMachine>> _executes =
            new Dictionary<char, Action<IVirtualMachine>>();

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            MemoryPointer = 0;
            InstructionPointer = 0;

        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            _executes.Add(symbol, execute);
        }

        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }

        public void Run()
        {
            for (; InstructionPointer < Instructions.Length; InstructionPointer++)
            {
                _executes.TryGetValue(Instructions[InstructionPointer], out var execute);
                execute?.Invoke(this);
            }
        }
    }
}