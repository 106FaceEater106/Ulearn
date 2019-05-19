using System.Collections.Generic;


namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var x = new Stack<MutablePair<int, int>>();
            var loops = ass(vm.Instructions);
            vm.RegisterCommand('[', b =>
            {
                /*var count = b.Memory[b.MemoryPointer];
                if (count == 0)
                    //b.InstructionPointer = loops.FindIndex();
                else
                    x.Push(new MutablePair<int, int>(b.InstructionPointer, count));*/
            });

            vm.RegisterCommand(']', b =>
            {
                var count = x.Peek().Item2;
                if (count < 2)
                {
                    x.Pop();
                    return;
                }

                x.Peek().Item2--;
                b.InstructionPointer = x.Peek().Item1;
            });
        }

        private static List<int> ass(string str)
        {
            var res = new List<int>();
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i].Equals(']')) res.Add(i);
            }

            return res;
        }
    }

    public class MutablePair<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public MutablePair(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}