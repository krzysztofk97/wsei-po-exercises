using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WarsztatTTPLib;

namespace WarsztatTTPTests
{
    [TestClass]
    public class TimePeriodTestUnitsConstructors
    {
        [TestMethod, TestCategory("Constructors")]
        public void Default_Constructor()
        {
            TimePeriod timePeriod = new TimePeriod();

            Assert.AreEqual(timePeriod.Period, 0);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow(0)]
        [DataRow(76576)]
        [DataRow(98937279)]
        [DataRow(8034740784370)]
        public void Constructor_1Param_Period(long period)
        {
            TimePeriod timePeriod = new TimePeriod(period);
            Assert.AreEqual(timePeriod.Period, period);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1Param_Period_ArgumentOutOfRangeExcepiton(int hours)
        {
            TimePeriod timePeriod = new TimePeriod(hours);
            Assert.AreEqual(timePeriod.Hours, hours);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)0)]
        [DataRow((int)76)]
        [DataRow((int)6)]
        public void Constructor_1Param(int hours)
        {
            TimePeriod timePeriod = new TimePeriod(hours);
            Assert.AreEqual(timePeriod.Hours, hours);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1Param_ArgumentOutOfRangeExcepiton(int hours)
        {
            TimePeriod timePeriod = new TimePeriod(hours);
            Assert.AreEqual(timePeriod.Hours, hours);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)0, (int)0)]
        [DataRow((int)76, (int)12)]
        [DataRow((int)6, (int)09)]
        public void Constructor_2Params(int hours, int minutes)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes);
            Assert.AreEqual(timePeriod.Hours, hours);
            Assert.AreEqual(timePeriod.Minutes, minutes);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)0, (int)60)]
        [DataRow((int)0, (int)-60)]
        [DataRow((int)-1, (int)-60)]
        [DataRow((int)-1, (int)0)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2Params_ArgumentOutOfRangeExcepiton(int hours, int minutes)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes);
            Assert.AreEqual(timePeriod.Hours, hours);
            Assert.AreEqual(timePeriod.Minutes, minutes);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)0, (int)0, (int)0)]
        [DataRow((int)76, (int)12, (int)10)]
        [DataRow((int)6, (int)09, (int)30)]
        public void Constructor_3Params(int hours, int minutes, int seconds)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes, seconds);
            Assert.AreEqual(timePeriod.Hours, hours);
            Assert.AreEqual(timePeriod.Minutes, minutes);
            Assert.AreEqual(timePeriod.Seconds, seconds);
        }

        [TestMethod, TestCategory("Contstructors")]
        [DataRow((int)0, (int)60, (int)0)]
        [DataRow((int)0, (int)60, (int)60)]
        [DataRow((int)0, (int)0, (int)60)]
        [DataRow((int)-60, (int)60, (int)0)]
        [DataRow((int)0, (int)-50, (int)60)]
        [DataRow((int)0, (int)0, (int)-30)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3Params_ArgumentOutOfRangeExcepiton(int hours, int minutes, int seconds)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes, seconds);
            Assert.AreEqual(timePeriod.Hours, hours);
            Assert.AreEqual(timePeriod.Minutes, minutes);
            Assert.AreEqual(timePeriod.Seconds, seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12:02:45", 12, 02, 45)]
        [DataRow("209:55:01", 209, 55, 01)]
        [DataRow("30:00:00", 30, 00, 00)]
        [DataRow("10:00:00", 10, 00, 00)]
        [DataRow("00:30:00", 00, 30, 00)]
        [DataRow("100:00:40", 100, 00, 40)]
        [DataRow("25", 25, 00, 00)]
        [DataRow("02:30", 02, 30, 00)]
        public void Constructor_String(string timePeriodString, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);

            Assert.AreEqual(timePeriod.Hours, expectedHours);
            Assert.AreEqual(timePeriod.Minutes, expectedMinutes);
            Assert.AreEqual(timePeriod.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("asdf")]
        [DataRow("23;53;11")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Wrong_String_ArgumentException(string timePeriodString)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Empty_String_ArgumentException(string timePeriodString)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:60:60")]
        [DataRow("01:100")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_String_ArgumentOutOfRangeException(string timePeriodString)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);
        }
    }

    [TestClass]
    public class TimePeriodUnitTestsOperators
    {
        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 34, 56)]
        [DataRow(100, 00, 00)]
        [DataRow(25, 59, 59)]
        public void Operator_Equals_True(int hours, int minutes, int seconds)
        {
            TimePeriod timePeriod1 = new TimePeriod(hours, minutes, seconds);
            TimePeriod timePeriod2 = new TimePeriod(hours, minutes, seconds);

            Assert.IsTrue(timePeriod1 == timePeriod2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 34, 56, 09, 22, 23)]
        [DataRow(06, 13, 11, 15, 32, 03)]
        [DataRow(16, 23, 55, 20, 12, 42)]
        public void Operator_NotEquals_True(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2)
        {
            TimePeriod timePeriod1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod timePeriod2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.IsTrue(timePeriod1 != timePeriod2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 34, 56, 09, 22, 23)]
        [DataRow(06, 13, 11, 15, 32, 03)]
        [DataRow(16, 23, 55, 20, 12, 42)]
        public void Operator_Equals_False(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2)
        {
            TimePeriod timePeriod1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod timePeriod2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.IsFalse(timePeriod1 == timePeriod2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 34, 56)]
        [DataRow(00, 00, 00)]
        [DataRow(23, 59, 59)]
        public void Operator_NotEquals_False(int hours, int minutes, int seconds)
        {
            TimePeriod timePeriod1 = new TimePeriod(hours, minutes, seconds);
            TimePeriod timePeriod2 = new TimePeriod(hours, minutes, seconds);

            Assert.IsFalse(timePeriod1 != timePeriod2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(09, 34, 56, 12, 22, 23)]
        [DataRow(06, 13, 11, 15, 32, 03)]
        [DataRow(16, 23, 55, 20, 12, 42)]
        public void Operator_Greater_Lower(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2)
        {
            TimePeriod timePeriod1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod timePeriod2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.IsTrue(timePeriod1 < timePeriod2);
            Assert.IsFalse(timePeriod1 > timePeriod2);
            Assert.IsFalse(timePeriod2 < timePeriod1);
            Assert.IsTrue(timePeriod2 > timePeriod1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(09, 34, 56, 12, 22, 23)]
        [DataRow(06, 13, 11, 15, 32, 03)]
        [DataRow(16, 23, 55, 20, 12, 42)]
        [DataRow(09, 34, 56, 09, 34, 56)]
        [DataRow(06, 13, 11, 06, 13, 11)]
        [DataRow(16, 23, 55, 16, 23, 55)]
        public void Operator_Greater_Lower_Or_Equal(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2)
        {
            TimePeriod timePeriod1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod timePeriod2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.IsTrue(timePeriod1 <= timePeriod2);
            Assert.IsTrue(timePeriod2 >= timePeriod1);
        }

        [DataTestMethod, TestCategory("Operator")]
        [DataRow("23:59:59", "00:00:02", "24:00:01")]
        [DataRow("12:31:31", "12:31:31", "25:03:02")]
        [DataRow("100:00:31", "00:59:31", "101:00:02")]
        public void Operator_Plus_TimePeriod_TimePeriod(string timePeriodString1, string timePeriodString2, string expectedTimePeriodString)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriodString1);
            TimePeriod timePeriod2 = new TimePeriod(timePeriodString2);

            TimePeriod expectedTimePeriod = new TimePeriod(expectedTimePeriodString);

            Assert.IsTrue(timePeriod1 + timePeriod2 == expectedTimePeriod);
        }

        [DataTestMethod, TestCategory("Operator")]
        [DataRow("23:59:59", "12:59:09", "11:00:50")]
        [DataRow("12:31:31", "13:31:31", "00:00:00")]
        [DataRow("00:00:31", "00:59:31", "00:00:00")]
        public void Operator_Minus_TimePeriod_TimePeriod(string timePeriodString1, string timePeriodString2, string expectedTimePeriodString)
        {
            TimePeriod timePeriod1 = new TimePeriod(timePeriodString1);
            TimePeriod timePeriod2 = new TimePeriod(timePeriodString2);

            TimePeriod expectedTimePeriod = new TimePeriod(expectedTimePeriodString);

            Assert.IsTrue(timePeriod1 - timePeriod2 == expectedTimePeriod);
        }

        [DataTestMethod, TestCategory("Opeartor")]
        [DataRow(2, "00:00:01", "00:00:02")]
        [DataRow(1.5, "12:00:00", "18:00:00")]
        [DataRow(3, "12:30:15", "37:30:45")]
        public void Operator_Multiplier_TimePeriod(double multiplier, string timePeriodString, string expectedTimePeriodString)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);

            TimePeriod expectedTimePeriod = new TimePeriod(expectedTimePeriodString);

            Assert.IsTrue(multiplier * timePeriod == expectedTimePeriod);
        }

        [DataTestMethod, TestCategory("Opeartor")]
        [DataRow("12:00:00", 2, "6:00:00")]
        [DataRow("01:00:00", 1.5, "00:40:00")]
        [DataRow("12:30:15", 3, "04:10:05")]
        public void Operator_Divider_TimePeriod(string timePeriodString, double divider, string expectedTimePeriodString)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);

            TimePeriod expectedTimePeriod = new TimePeriod(expectedTimePeriodString);

            Assert.IsTrue(timePeriod / divider == expectedTimePeriod);
        }
    }

    [TestClass]
    public class TimePeriodUnitTestsMethods
    {
        [DataTestMethod, TestCategory("Methods")]
        [DataRow(12, 02, 45, "12:02:45")]
        [DataRow(209, 55, 01, "209:55:01")]
        [DataRow(30, 00, 00, "30:00:00")]
        public void ToString_Override(int hours, int minutes, int seconds, string expectedString)
        {
            TimePeriod timePeriod = new TimePeriod(hours, minutes, seconds);

            Assert.AreEqual(timePeriod.ToString(), expectedString);
            Assert.AreEqual($"{timePeriod}", expectedString);
        }
    }
}