using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        public int Length => _list.Length;
        private readonly byte[] _list;
        private int _hash;

        public ReadonlyBytes(params byte[] array)
        {
            _list = array ?? throw new ArgumentNullException();
            HashCodeInside();
        }

        public byte this[int index]
        {
            get
            {
                if (index >= _list.Length) throw new IndexOutOfRangeException();
                return _list[index];
            }
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj?.GetType() == typeof(ReadonlyBytes) &&
                  Length == ((ReadonlyBytes) obj).Length)) return false;

            return Equals((ReadonlyBytes)obj);
        }

        private bool Equals(ReadonlyBytes other)
        {
            var isItemsEqual = true;
            var count = 0;
            while (isItemsEqual && count < Length)
            {
                isItemsEqual = this[count] == other[count];
                ++count;
            }

            return isItemsEqual;
        }

        private void HashCodeInside()
        {
            const int fnvPrime = 3123423;
            foreach (var item in _list)
            {
                unchecked
                {
                    _hash *= fnvPrime;
                    _hash ^= item;
                }
            }
        }

        public override int GetHashCode() => _hash;

        public override string ToString() => $"[{string.Join(", ", _list)}]";

        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>) _list).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}