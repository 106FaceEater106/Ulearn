using System.Collections.Generic;


namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private readonly int _limit;
        private readonly LinkedList<T> _linkedList = new LinkedList<T>();

        public LimitedSizeStack(int limit)
        {
            _limit = limit;
        }

        public void Push(T item)
        {
            if (!(Count < _limit))
                _linkedList.RemoveFirst();
            _linkedList.AddLast(item);
        }

        public T Pop()
        {
            var result = _linkedList.Last.Value;
            _linkedList.RemoveLast();
            return result;
        }

        public int Count => _linkedList.Count;
    }
}