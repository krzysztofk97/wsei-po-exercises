using System;
using System.Collections;
using System.Collections.Generic;

namespace MultiSetLib
{
    public class MultiSet<T> : ICollection<T>, IEnumerable<T>
    {
        private Dictionary<T, int> multiset = new Dictionary<T, int>();

        readonly bool _isReadOnly;
        public bool IsReadOnly => _isReadOnly;

        public int this[T item] => multiset[item];

        public static MultiSet<T> Empty => new MultiSet<T>();

        public MultiSet() { }

        public MultiSet(IEnumerable<T> sequence, bool isReadOnly = false)
        {
            foreach(var item in sequence)
                this.Add(item);

            _isReadOnly = isReadOnly;
        }

        public MultiSet(IEqualityComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer, bool isReadOnly = false)
        {
            foreach (var item in sequence)
                this.Add(item);

            _comparer = comparer;
            _isReadOnly = isReadOnly;
        }

        public int Count
        {
            get 
            {
                int count = 0;

                foreach(var item in multiset)
                    count += item.Value;

                return count;
            }
        }

        public void Add(T item)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if(multiset.ContainsKey(item))
                multiset[item]++;
            else
                multiset.Add(item, 1);
        }

        public MultiSet<T> Add(T item, int numberOfItems = 1)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if(multiset.ContainsKey(item))
                multiset[item] += numberOfItems;
            else
                multiset.Add(item, numberOfItems);

            return this;
        }

        public void Clear()
        {
            if(IsReadOnly) throw new NotSupportedException();

            multiset.Clear();
        }

        public bool Contains(T item) => multiset.ContainsKey(item);

        public bool IsEmpty => Count == 0;

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + this.Count) throw new ArgumentException();

            int index = arrayIndex;

            foreach(var item in this)
            {
                array[index] = item;
                index++;
            }
        }

        public bool Remove(T item)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if (!multiset.ContainsKey(item))
                return false;

            if (multiset[item] > 1)
                multiset[item]--;
            else
                multiset.Remove(item);

            return true;
        }

        public MultiSet<T> RemoveAll(T item)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if (multiset.ContainsKey(item))
                multiset.Remove(item);
            
            return this;
        }

        public MultiSet<T> Remove(T item, int numberOfItems)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if (!multiset.ContainsKey(item))
                return this;

            if(multiset[item] <= numberOfItems)
                multiset.Remove(item);
            else
                multiset[item] -= numberOfItems;
            
            return this;
        }

        public MultiSet<T> UnionWith(IEnumerable<T> other)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if(other == null) throw new ArgumentNullException();

            foreach(var item in other)
                this.Add(item);

            return this;
        }

        public MultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if (other == null) throw new ArgumentNullException();

            MultiSet<T> otherAsMultiSet = new MultiSet<T>(other);

            foreach(var item in otherAsMultiSet)
            {
                if(!this.Contains(item))
                    this.Remove(item);
                else if(this[item] < otherAsMultiSet[item])
                    this.Add(item, otherAsMultiSet[item] - this[item]);
                else if(this[item] > otherAsMultiSet[item])
                    this.Remove(item, this[item] - otherAsMultiSet[item]);
            }

            foreach(var item in this)
                if(!otherAsMultiSet.Contains(item))
                    this.Remove(item);

            return this;
        }

        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            if(IsReadOnly) throw new NotSupportedException();

            if(other == null) throw new ArgumentNullException();

            foreach(var item in other)
                this.Remove(item);

            return this;
        }

        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (other == null) throw new ArgumentNullException();

            MultiSet<T> tmp = new MultiSet<T>(this);

            tmp.IntersectWith(other);

            this.UnionWith(other);

            foreach(var item in tmp)
                this.Remove(item, 2);

            return this;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if(other == null) throw new ArgumentNullException();

            MultiSet<T> otherAsMultiSet = new MultiSet<T>(other);

            if(this.Count > otherAsMultiSet.Count) return false;

            foreach(var item in this)
            {
                if(otherAsMultiSet.Contains(item))
                {
                    if(this[item] > otherAsMultiSet[item])
                        return false;
                } 
                else return false;
            }

            return true;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if(other == null) throw new ArgumentNullException();

            MultiSet<T> otherAsMultiSet = new MultiSet<T>(other);

            if(this.Count >= otherAsMultiSet.Count) return false;

            return this.IsSubsetOf(otherAsMultiSet);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if(other == null) throw new ArgumentNullException();

            MultiSet<T> otherAsMultiSet = new MultiSet<T>(other);

            return otherAsMultiSet.IsSubsetOf(this);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if(other == null) throw new ArgumentNullException();            

            MultiSet<T> otherAsMultiSet = new MultiSet<T>(other);

            if(this.Count <= otherAsMultiSet.Count) return false;

            return otherAsMultiSet.IsSubsetOf(this);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if(other == null) throw new ArgumentNullException();

            foreach(var item in other)
                if(this.Contains(item))
                    return true;

            return false;
        }

        public bool MultiSetEquals(IEnumerable<T> other)
        {
            if(other == null) throw new ArgumentNullException();
            
            MultiSet<T> otherAsMultiSet = new MultiSet<T>(other);

            if(this.Count != otherAsMultiSet.Count) return false;

            foreach(var item in this)
            {
                if(otherAsMultiSet.Contains(item))
                {
                    if(this[item] != otherAsMultiSet[item])
                        return false;
                } 
                else return false;
            }

            return true;
        }

        private IEqualityComparer<T> _comparer;
        public IEqualityComparer<T> Comparer => _comparer;

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var(item, multiplicity) in multiset)
                for (int i = 0; i < multiplicity; i++)
                    yield return item;
        }

        public static MultiSet<T> operator +(MultiSet<T> first, MultiSet<T> second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            return new MultiSet<T>(first.UnionWith(second));
        }

        public static MultiSet<T> operator -(MultiSet<T> first, MultiSet<T> second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            return new MultiSet<T>(first.ExceptWith(second));
        }

        public static MultiSet<T> operator *(MultiSet<T> first, MultiSet<T> second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            return new MultiSet<T>(first.IntersectWith(second));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IReadOnlyDictionary<T, int> AsDictionary() => new Dictionary<T, int>(multiset);

        public IReadOnlySet<T> AsSet() => new HashSet<T>(this);
    }
}