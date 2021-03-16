using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MultiSetUnitTests
{
    public class TestEqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            throw new NotImplementedException();
        }
    }
}