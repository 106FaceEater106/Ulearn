using System;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private readonly List<Clone> _clones;

        public CloneVersionSystem()
        {
            _clones = new List<Clone>
            {
                new Clone()
            };
        }

        public string Execute(string query)
        {
            var queryAsArray = query.Split(' ');
            var command = queryAsArray[0];
            var cloneNumber = int.Parse(queryAsArray[1]) - 1;
            var currentClone = _clones[cloneNumber];
            if (command == "learn")
            {
                var programNumber = int.Parse(queryAsArray[2]);
                currentClone.Learn(programNumber);
            }
            else if (command == "rollback")
            {
                currentClone.RollBack();
            }
            else if (command == "relearn")
            {
                currentClone.Relearn();
            }
            else if (command == "clone")
            {
                _clones.Add(new Clone(currentClone));
            }
            else if (command == "check")
            {
                return currentClone.Check();
            }

            return null;
        }
    }

    public class Clone
    {
        private readonly MyStack<int> _clonePrograms;
        private readonly MyStack<int> _rollbackHistory;

        public Clone()
        {
            _clonePrograms = new MyStack<int>();
            _rollbackHistory = new MyStack<int>();
        }

        public Clone(Clone a)
        {
            _clonePrograms = a._clonePrograms.Clone();
            _rollbackHistory = a._rollbackHistory.Clone();
        }

        public void Learn(int program)
        {
            _clonePrograms.Push(program);
            _rollbackHistory.Clear();
        }

        public void RollBack()
        {
            if (_clonePrograms.Count == 0) return;
            _rollbackHistory.Push(_clonePrograms.Pop());
        }

        public void Relearn()
        {
            if (_rollbackHistory.Count == 0) return;
            _clonePrograms.Push(_rollbackHistory.Pop());
        }

        public string Check()
        {
            return _clonePrograms.Count > 0 ? _clonePrograms.Peek().ToString() : "basic";
        }
    }

    public class MyStack<T>
    {
        private class StackItem<TItem>
        {
            public TItem Value { get; set; }
            public StackItem<TItem> PrevItem { get; set; }
            public StackItem<TItem> NextItem { get; set; }
        }

        private StackItem<T> _begin;
        private StackItem<T> _end;
        public int Count { get; private set; }

        private MyStack(StackItem<T> end, StackItem<T> begin, int count)
        {
            Count = count;
            _end = end;
            _begin = begin;
        }

        public MyStack()
        {
            _begin = new StackItem<T>();
            _end = new StackItem<T>();
        }

        public void Push(T item)
        {
            if (_begin == null)
            {
                _begin = _end = new StackItem<T>() {Value = item, PrevItem = null, NextItem = null};
                Count = 1;
            }
            else
            {
                var newItem = new StackItem<T>() {Value = item, PrevItem = _end, NextItem = null};
                _end.NextItem = newItem;
                _end = newItem;
                Count++;
            }
        }

        public T Pop()
        {
            if (_begin == null) throw new InvalidOperationException();
            var result = _end.Value;
            if (_begin == _end)
                _begin = _end = null;
            else
            {
                _end = _end.PrevItem;
                _end.NextItem = null;
            }

            Count--;
            return result;
        }

        public T Peek()
        {
            return _end.Value;
        }

        public void Clear()
        {
            _begin = _end = null;
            Count = 0;
        }

        public MyStack<T> Clone()
        {
            return new MyStack<T>(_end, _begin, Count);
        }
    }
}