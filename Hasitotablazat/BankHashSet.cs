using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasitotablazat
{
    public class BankHashSet<K, T>
    {
        public IEnumerable<List<BankHashSetItem<K, T>>> GetContents()
        {
            foreach (var list in _contents)
            {
                yield return list;
            }
        }
        private List<BankHashSetItem<K, T>>[] _contents;
        private int _size;
        private HashCallback _hashFunction;

        public delegate int HashCallback(K key, int size);

        public BankHashSet(int size, HashCallback hashFunction = null)
        {
            _size = size;
            _contents = new List<BankHashSetItem<K, T>>[size];

            for (int i = 0; i < size; i++)
            {
                _contents[i] = new List<BankHashSetItem<K, T>>();
            }

            _hashFunction = hashFunction ?? DefaultHashing;
        }

        public BankHashSet() : this(100, null) { }

        public void Insert(K key, T content)
        {
            int index = _hashFunction(key, _size);
            var item = new BankHashSetItem<K, T> { Key = key, Content = content };
            _contents[index].Add(item);
        }

        public T Find(K key)
        {
            int index = _hashFunction(key, _size);
            foreach (var item in _contents[index])
            {
                if (item.Key.Equals(key))
                {
                    return item.Content;
                }
            }

            throw new KeyNotFoundException($"Key '{key}' not found.");
        }

        public T this[K key]
        {
            get { return Find(key); }
            set { Insert(key, value); }
        }

        private static int DefaultHashing(K key, int size)
        {
            return Math.Abs(key.GetHashCode()) % size;
        }

        public class BankHashSetItem<K, T>
        {
            public K Key { get; set; }
            public T Content { get; set; }
        }
    }
}
