using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiSetLib;
using System.Collections.Generic;

namespace MultiSetUnitTests
{
    [TestClass]
    public class MultiSetTest
    {
        TestEqualityComparer<char> testEqualityComparer = new TestEqualityComparer<char>();

        [DataTestMethod, TestCategory("Constructors")]
        public void Default_Constructor()
        {
            MultiSet<char> multiset = new MultiSet<char>();
        }

        [DataTestMethod, TestCategory("Constructors")]
        public void Constructor_Comparer()
        {
            MultiSet<char> multiset = new MultiSet<char>(testEqualityComparer);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(new char[]{ 'a', 'b', 'c' })]
        public void Constructor_Sequence(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(new char[]{ 'a', 'b', 'c' })]
        public void Constructor_Sequence_Comparer(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, testEqualityComparer);
        }

        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[]{ 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void ICollection_Count(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);

            Assert.AreEqual(multiset.Count, 6);
        }
        
        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[]{ 'a', 'a', 'a', 'b', 'b' })]
        public void ICollection_Add(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            multiset.Add('c');

            Assert.AreEqual(multiset['c'], 1);
        }

        [DataTestMethod, TestCategory("ICollection Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void ICollection_Add_IsReadOnly_NotSupportedException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, isReadOnly: true);
            multiset.Add('c');
        }

        [DataTestMethod, TestCategory("Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b' })]
        public void Add_Multiple(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            multiset.Add('c', 2);

            Assert.AreEqual(multiset['c'], 2);
        }

        [DataTestMethod, TestCategory("Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void Add_Multiple_IsReadOnly_NotSupportedException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, isReadOnly: true);
            multiset.Add('c', 2);
        }

        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void ICollection_Remove(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            multiset.Remove('b');

            Assert.AreEqual(multiset['b'], 1);
        }

        [DataTestMethod, TestCategory("ICollection Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void ICollection_Remove_IsReadOnly_NotSupportedException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, isReadOnly: true);
            multiset.Remove('b');
        }

        [DataTestMethod, TestCategory("Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void Remove_Multiple(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            multiset.Remove('a', 2);

            Assert.AreEqual(multiset['a'], 1);
        }

        [DataTestMethod, TestCategory("Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void Remove_Multiple_IsReadOnly_NotSupportedException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, isReadOnly: true);
            multiset.Remove('a', 2);
        }

        [DataTestMethod, TestCategory("Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void RemoveAll(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            multiset.RemoveAll('a');

            Assert.IsFalse(multiset.Contains('a') && multiset['b'] == 2 && multiset['c'] == 1);
        }

        [DataTestMethod, TestCategory("Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void RemoveAll_IsReadOnly_NotSupportedException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, isReadOnly: true);
            multiset.RemoveAll('a');
        }

        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void ICollection_Contains(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);

            Assert.IsTrue(multiset.Contains('b') && !multiset.Contains('d'));
        }

        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void ICollection_Clear(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            multiset.Clear();

            Assert.AreEqual(multiset.Count, 0);
        }

        [DataTestMethod, TestCategory("ICollection Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void ICollection_Clear_IsReadOnly_NotSupportedException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data, isReadOnly: true);
            multiset.Clear();
        }

        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'x', 'y', 'a', 'b', 'c'})]
        public void ICollection_CopyTo(IEnumerable<char> data, char[] expectedArray)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            char[] copyToArray = { 'x', 'y', 'z', 'x', 'y' };

            multiset.CopyTo(copyToArray, 2);

            CollectionAssert.AreEqual(copyToArray, expectedArray);
        }

        [DataTestMethod, TestCategory("ICollection Methods Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentException))]
        public void ICollection_CopyTo_TooShortArray_ArgumentException(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            char[] copyToArray = { 'x', 'y', 'z' };

            multiset.CopyTo(copyToArray, 2);
        }

        [DataTestMethod, TestCategory("ICollection Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void IEnumerable_Foreach(IEnumerable<char> data)
        {
            MultiSet<char> multiset = new MultiSet<char>(data);
            List<char> multisetList = new List<char>();
            List<char> exceptedList = new List<char>(data);

            foreach (var item in multiset)
                multisetList.Add(item);

            CollectionAssert.AreEqual(multisetList, exceptedList);
        }

        [DataTestMethod, TestCategory("Operations on Sets")]
        [DataRow(new char[] { 'a', 'a', 'b' }, new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void UnionWith(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.UnionWith(b);

            List<char> multisetAsList = new List<char>(multiset);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operations on Sets")]
        [DataRow(new char[] { 'a', 'a', 'b' }, new char[] { 'a', 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void UnionWith_IsReadOnly_NotSupportedException(char[] a, char[] b)
        {
            MultiSet<char> multiset = new MultiSet<char>(a, isReadOnly: true);

            multiset.UnionWith(b);
        }

        [DataTestMethod, TestCategory("Operations on Sets")]
        [DataRow(new char[] { 'a', 'a', 'b' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnionWith_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.UnionWith(null);
        }

        [DataTestMethod, TestCategory("Operations on Sets")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'b', 'c' })]
        public void IntersectWith(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.IntersectWith(b);

            List<char> multisetAsList = new List<char>(multiset);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operations on Sets Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void IntersectWith_IsReadOnly_NotSupportedException(char[] a, char[] b)
        {
            MultiSet<char> multiset = new MultiSet<char>(a, isReadOnly: true);

            multiset.IntersectWith(b);
        }

        [DataTestMethod, TestCategory("Operations on Sets Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IntersectWith_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.IntersectWith(null);
        }

        [DataTestMethod, TestCategory("Operations on Sets")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'a' })]
        public void ExceptWith(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.ExceptWith(b);

            List<char> multisetAsList = new List<char>(multiset);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operations on Sets Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void ExceptWith_IsReadOnly_NotSupportedException(char[] a, char[] b)
        {
            MultiSet<char> multiset = new MultiSet<char>(a, isReadOnly: true);

            multiset.ExceptWith(b);
        }

        [DataTestMethod, TestCategory("Operations on Sets Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExceptWith_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.ExceptWith(null);
        }

        [DataTestMethod, TestCategory("Operations on Sets")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'a', 'd' })]
        public void SymmetricExceptWith(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.SymmetricExceptWith(b);

            List<char> multisetAsList = new List<char>(multiset);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operations on Sets Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void SymmetricExceptWith_IsReadOnly_NotSupportedException(char[] a, char[] b)
        {
            MultiSet<char> multiset = new MultiSet<char>(a, isReadOnly: true);

            multiset.SymmetricExceptWith(b);
        }

        [DataTestMethod, TestCategory("Operations on Sets Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SymmetricExceptWith_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.SymmetricExceptWith(null);
        }

        [DataTestMethod, TestCategory("Subsets Check")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'd' }, false)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, true)]
        public void IsSubsetOf(char[] a, char[] b, bool expectedValue)
        {
            MultiSet<char> multiset = new MultiSet<char>(b);

            Assert.AreEqual(multiset.IsSubsetOf(a), expectedValue);
        }

        [DataTestMethod, TestCategory("Subsets Check Exception")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsSubsetOf_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.IsSubsetOf(null);
        }

        [DataTestMethod, TestCategory("Subsets Check")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'd' }, false)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, false)]
        public void IsProperSubsetOf(char[] a, char[] b, bool expectedValue)
        {
            MultiSet<char> multiset = new MultiSet<char>(b);

            Assert.AreEqual(multiset.IsProperSubsetOf(a), expectedValue);
        }

        [DataTestMethod, TestCategory("Subsets Check Exception")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsProperSubsetOf_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.IsProperSubsetOf(null);
        }

        [DataTestMethod, TestCategory("Subsets Check")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'd' }, false)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, true)]
        public void IsSupersetOf(char[] a, char[] b, bool expectedValue)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            Assert.AreEqual(multiset.IsSupersetOf(b), expectedValue);
        }

        [DataTestMethod, TestCategory("Subsets Check Exception")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsSupersetOf_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.IsSupersetOf(null);
        }

        [DataTestMethod, TestCategory("Subsets Check")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'd' }, false)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, false)]
        public void IsProperSupersetOf(char[] a, char[] b, bool expectedValue)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            Assert.AreEqual(multiset.IsProperSupersetOf(b), expectedValue);
        }

        [DataTestMethod, TestCategory("Subsets Check Exception")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsProperSupersetOf_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.IsProperSupersetOf(null);
        }

        [DataTestMethod, TestCategory("Methods")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'a', 'b', 'c' }, true)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, true)]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' }, new char[] { 'd' }, false)]
        public void Overlaps(char[] a, char[] b, bool expectedValue)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a);

            Assert.AreEqual(multisetA.Overlaps(b), expectedValue);
        }

        [DataTestMethod, TestCategory("Methods Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Overlaps_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.Overlaps(null);
        }

        [DataTestMethod, TestCategory("Methods")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'c', 'a', 'b' }, true)]
        [DataRow(new char[] { 'a', 'c', 'c' }, new char[] { 'c', 'a', 'c' }, true)]
        [DataRow(new char[] { 'a', 'c', 'c' }, new char[] { 'b', 'a', 'c' }, false)]
        public void MultiSetEquals(char[] a, char[] b, bool expectedValue)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            Assert.AreEqual(multisetA.MultiSetEquals(multisetB), expectedValue);
        }

        [DataTestMethod, TestCategory("Methods Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultiSetEquals_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset.MultiSetEquals(null);
        }

        [TestMethod, TestCategory("Methods")]
        public void IsEmpty()
        {
            MultiSet<char> multisetA = new MultiSet<char>();
            MultiSet<char> multisetB = new MultiSet<char>(){ 'a', 'b', 'c' };

            Assert.IsTrue(multisetA.IsEmpty && !multisetB.IsEmpty);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(new char[] { 'a', 'a', 'b' }, new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        public void Operator_Plus(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            multisetA += multisetB;

            List<char> multisetAsList = new List<char>(multisetA);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operators Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'b' }, new char[] { 'a', 'b', 'c' }, new char[] { 'a', 'a', 'a', 'b', 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void Operator_Plus_IsReadOnly_NotSupportedException(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a, isReadOnly: true);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            multisetA += multisetB;
        }

        [DataTestMethod, TestCategory("Operators Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'b' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Operator_Plus_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset += null;
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'a' })]
        public void Operator_Minus(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            multisetA -= multisetB;

            List<char> multisetAsList = new List<char>(multisetA);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operators Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'a' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void Operator_Minus_IsReadOnly_NotSupportedException(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a, isReadOnly: true);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            multisetA -= multisetB;
        }

        [DataTestMethod, TestCategory("Operators Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'b' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Operator_Minus_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset -= null;
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'b', 'c' })]
        public void Operator_Multiplicity(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            multisetA *= multisetB;

            List<char> multisetAsList = new List<char>(multisetA);
            List<char> expectedAsList = new List<char>(expected);

            CollectionAssert.AreEqual(multisetAsList, expectedAsList);
        }

        [DataTestMethod, TestCategory("Operators Exceptions")]
        [DataRow(new char[] { 'a', 'b', 'c' }, new char[] { 'b', 'c', 'd' }, new char[] { 'b', 'c' })]
        [ExpectedException(typeof(NotSupportedException))]
        public void Operator_Multiplicity_IsReadOnly_NotSupportedException(char[] a, char[] b, char[] expected)
        {
            MultiSet<char> multisetA = new MultiSet<char>(a, isReadOnly: true);
            MultiSet<char> multisetB = new MultiSet<char>(b);

            multisetA *= multisetB;
        }

        [DataTestMethod, TestCategory("Operators Exceptions")]
        [DataRow(new char[] { 'a', 'a', 'b' })]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Operator_Multiplicity_OtherIsNull_ArgumentNullException(char[] a)
        {
            MultiSet<char> multiset = new MultiSet<char>(a);

            multiset *= null;
        }
    }
}