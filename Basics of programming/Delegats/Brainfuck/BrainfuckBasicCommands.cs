using System;
using System.Text.RegularExpressions;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write((char) b.Memory[b.MemoryPointer]); });

            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte) read(); });

            vm.RegisterCommand('+', b =>
            {
                unchecked
                {
                    b.Memory[b.MemoryPointer]++;
                }
            });

            vm.RegisterCommand('-', b =>
            {
                unchecked
                {
                    b.Memory[b.MemoryPointer]--;
                }
            });

            vm.RegisterCommand('>', b =>
            {
                if (++b.MemoryPointer >= b.Memory.Length) b.MemoryPointer %= b.Memory.Length;
            });

            vm.RegisterCommand('<', b =>
            {
                if (--b.MemoryPointer < 0) b.MemoryPointer = b.Memory.Length - 1;
            });

            RegisterConstants(vm);
        }

        private static void RegisterConstants(IVirtualMachine vm)
        {
            for (var i = 0; i < byte.MaxValue; i++)
            {
                var item = ((char) i).ToString();
                var reg = new Regex(@"[a-zA-Z0-9]");
                if (!reg.IsMatch(item)) continue;
                var i1 = i;
                vm.RegisterCommand((char) i, b => { b.Memory[b.MemoryPointer] = (byte) i1; });
            }
        }
    }
}