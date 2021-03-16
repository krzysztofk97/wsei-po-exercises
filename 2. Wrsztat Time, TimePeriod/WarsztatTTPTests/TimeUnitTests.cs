using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using WarsztatTTPLib;

namespace WarsztatTTPTests
{
    [TestClass]
    public class TimeUnitTestsConstructors
    {
        [DataTestMethod, TestCategory("Constructors")]
        public void Default_Constructor()
        {
            Time time = new Time();

            Assert.AreEqual(time.Hours, 0);
            Assert.AreEqual(time.Minutes, 0);
            Assert.AreEqual(time.Seconds, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)12, (byte)0, (byte)0)]
        [DataRow((byte)21, (byte)21, (byte)0, (byte)0)]
        [DataRow((byte)09, (byte)09, (byte)0, (byte)0)]
        public void Constructor_1Param(byte hours, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time time = new Time(hours);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)30)]
        [DataRow((byte)60)]
        [DataRow((byte)90)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1Param_ArgumentOutOfRangeExcepiton(byte hours)
        {
            Time time = new Time(hours);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)07, (byte)09, (byte)07, (byte)09, (byte)0)]
        [DataRow((byte)23, (byte)19, (byte)23, (byte)19, (byte)0)]
        [DataRow((byte)11, (byte)41, (byte)11, (byte)41, (byte)0)]
        public void Constructor_2Params(byte hours, byte minutes, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time time = new Time(hours, minutes);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)30, (byte)05)]
        [DataRow((byte)24, (byte)05)]
        [DataRow((byte)12, (byte)60)]
        [DataRow((byte)12, (byte)90)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2Params_ArgumentOutOfRangeExcepiton(byte hours, byte minutes)
        {
            Time time = new Time(hours, minutes);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)12, (byte)34, (byte)56, (byte)12, (byte)34, (byte)56)]
        [DataRow((byte)21, (byte)53, (byte)24, (byte)21, (byte)53, (byte)24)]
        [DataRow((byte)09, (byte)08, (byte)01, (byte)09, (byte)08, (byte)01)]
        [DataRow((byte)16, (byte)23, null, (byte)16, (byte)23, (byte)00)]
        [DataRow((byte)19, null, null, (byte)19, (byte)0, (byte)0)]
        public void Constructor_3Params(byte hours, byte minutes, byte seconds, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time time = new Time(hours, minutes, seconds);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)25, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)60, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)60)]
        [DataRow((byte)255, (byte)0, (byte)0)]
        [DataRow((byte)0, (byte)128, (byte)0)]
        [DataRow((byte)0, (byte)0, (byte)64)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3Params_ArgumentOutOfRangeExcepiton(byte hours, byte minutes, byte seconds)
        {
            Time time = new Time(hours, minutes, seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12:02:45", (byte)12, (byte)02, (byte)45)]
        [DataRow("09:55:01", (byte)09, (byte)55, (byte)01)]
        [DataRow("00:00:00", (byte)00, (byte)00, (byte)00)]
        [DataRow("10:00:00", (byte)10, (byte)00, (byte)00)]
        [DataRow("00:30:00", (byte)00, (byte)30, (byte)00)]
        [DataRow("00:00:40", (byte)00, (byte)00, (byte)40)]
        [DataRow("12", (byte)12, (byte)00, (byte)00)]
        [DataRow("02:30", (byte)02, (byte)30, (byte)00)]
        public void Constructor_String(string timeString, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time time = new Time(timeString);

            Assert.AreEqual(time.Hours, expectedHours);
            Assert.AreEqual(time.Minutes, expectedMinutes);
            Assert.AreEqual(time.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("asdf")]
        [DataRow("12.34.56")]
        [DataRow("23;53;11")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Wrong_String_ArgumentException(string timeString)
        {
            Time time = new Time(timeString);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Empty_String_ArgumentException(string timeString)
        {
            Time time = new Time(timeString);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("123")]
        [DataRow("123:1")]
        [DataRow("24:60:60")]
        [DataRow("01:100")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_String_ArgumentOutOfRangeException(string timeString)
        {
            Time time = new Time(timeString);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, "00:00:00")]
        [DataRow(60, "00:01:00")]
        [DataRow(86399, "23:59:59")]
        public void Constructor_InSeconds(int seconds, string expectedTimeString)
        {
            Time time = new Time(seconds);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(time == expectedTime);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(86400)]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_InSeconds_ArgumentOutOfRangeException(int seconds)
        {
            Time time = new Time(seconds);
        }
    }

    [TestClass]
    public class TimeUnitTestsOperators
    {
        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)34, (byte)56)]
        [DataRow((byte)00, (byte)00, (byte)00)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        public void Operator_Equals_True(byte hours, byte minutes, byte seconds)
        {
            Time time1 = new Time(hours, minutes, seconds);
            Time time2 = new Time(hours, minutes, seconds);

            Assert.IsTrue(time1 == time2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)34, (byte)56, (byte)09, (byte)22, (byte)23)]
        [DataRow((byte)06, (byte)13, (byte)11, (byte)15, (byte)32, (byte)03)]
        [DataRow((byte)16, (byte)23, (byte)55, (byte)20, (byte)12, (byte)42)]
        public void Operator_NotEquals_True(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2)
        {
            Time time1 = new Time(hours1, minutes1, seconds1);
            Time time2 = new Time(hours2, minutes2, seconds2);

            Assert.IsTrue(time1 != time2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)34, (byte)56, (byte)09, (byte)22, (byte)23)]
        [DataRow((byte)06, (byte)13, (byte)11, (byte)15, (byte)32, (byte)03)]
        [DataRow((byte)16, (byte)23, (byte)55, (byte)20, (byte)12, (byte)42)]
        public void Operator_Equals_False(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2)
        {
            Time time1 = new Time(hours1, minutes1, seconds1);
            Time time2 = new Time(hours2, minutes2, seconds2);

            Assert.IsFalse(time1 == time2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)12, (byte)34, (byte)56)]
        [DataRow((byte)00, (byte)00, (byte)00)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        public void Operator_NotEquals_False(byte hours, byte minutes, byte seconds)
        {
            Time time1 = new Time(hours, minutes, seconds);
            Time time2 = new Time(hours, minutes, seconds);

            Assert.IsFalse(time1 != time2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)09, (byte)34, (byte)56, (byte)12, (byte)22, (byte)23)]
        [DataRow((byte)06, (byte)13, (byte)11, (byte)15, (byte)32, (byte)03)]
        [DataRow((byte)16, (byte)23, (byte)55, (byte)20, (byte)12, (byte)42)]
        public void Operator_Greater_Lower(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2)
        {
            Time time1 = new Time(hours1, minutes1, seconds1);
            Time time2 = new Time(hours2, minutes2, seconds2);

            Assert.IsTrue(time1 < time2);
            Assert.IsFalse(time1 > time2);
            Assert.IsFalse(time2 < time1);
            Assert.IsTrue(time2 > time1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow((byte)09, (byte)34, (byte)56, (byte)12, (byte)22, (byte)23)]
        [DataRow((byte)06, (byte)13, (byte)11, (byte)15, (byte)32, (byte)03)]
        [DataRow((byte)16, (byte)23, (byte)55, (byte)20, (byte)12, (byte)42)]
        [DataRow((byte)09, (byte)34, (byte)56, (byte)09, (byte)34, (byte)56)]
        [DataRow((byte)06, (byte)13, (byte)11, (byte)06, (byte)13, (byte)11)]
        [DataRow((byte)16, (byte)23, (byte)55, (byte)16, (byte)23, (byte)55)]
        public void Operator_Greater_Lower_Or_Equal(byte hours1, byte minutes1, byte seconds1, byte hours2, byte minutes2, byte seconds2)
        {
            Time time1 = new Time(hours1, minutes1, seconds1);
            Time time2 = new Time(hours2, minutes2, seconds2);

            Assert.IsTrue(time1 <= time2);
            Assert.IsTrue(time2 >= time1);
        }

        [DataTestMethod, TestCategory("Operator")]
        [DataRow("23:59:59", "00:00:02", "00:00:01")]
        [DataRow("12:31:31", "12:31:31", "01:03:02")]
        [DataRow("00:00:31", "00:59:31", "01:00:02")]
        public void Operator_Plus_Time_Time(string timeString1, string timeString2, string expectedTimeString)
        {
            Time time1 = new Time(timeString1);
            Time time2 = new Time(timeString2);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(time1 + time2 == expectedTime);
        }

        [DataTestMethod, TestCategory("Opeartor")]
        [DataRow("00:00:00", "123:56:12", "3:56:12")]
        [DataRow("00:00:00", "24:00:00", "00:00:00")]
        [DataRow("23:59:59", "00:00:01", "00:00:00")]
        public void Operator_Plus_Time_TimePeriod(string timeString, string timePeriodString, string expectedTimeString)
        {
            Time time = new Time(timeString);
            TimePeriod timePeriod = new TimePeriod(timePeriodString);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(time + timePeriod == expectedTime);
        }

        [DataTestMethod, TestCategory("Operator")]
        [DataRow("23:59:59", "12:59:09", "11:00:50")]
        [DataRow("12:31:31", "13:31:31", "23:00:00")]
        [DataRow("00:00:31", "00:59:31", "23:01:00")]
        public void Operator_Minus_Time_Time(string timeString1, string timeString2, string expectedTimeString)
        {
            Time time1 = new Time(timeString1);
            Time time2 = new Time(timeString2);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(time1 - time2 == expectedTime);
        }

        [DataTestMethod, TestCategory("Opeartor")]
        [DataRow("00:00:00", "00:00:01", "23:59:59")]
        [DataRow("10:00:00", "25:00:00", "09:00:00")]
        [DataRow("12:30:15", "24:30:15", "12:00:00")]
        public void Operator_Minus_Time_TimePeriod(string timeString, string timePeriodString, string expectedTimeString)
        {
            Time time = new Time(timeString);
            TimePeriod timePeriod = new TimePeriod(timePeriodString);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(time - timePeriod == expectedTime);
        }

        [DataTestMethod, TestCategory("Opeartor")]
        [DataRow(2, "00:00:01", "00:00:02")]
        [DataRow(1.5, "12:00:00", "18:00:00")]
        [DataRow(3, "12:30:15", "13:30:45")]
        public void Operator_Multiplier_Time(double multiplier, string timeString, string expectedTimeString)
        {
            Time time = new Time(timeString);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(multiplier * time == expectedTime);
        }

        [DataTestMethod, TestCategory("Opeartor")]
        [DataRow("12:00:00", 2, "6:00:00")]
        [DataRow("01:00:00", 1.5, "00:40:00")]
        [DataRow("12:30:15", 3, "04:10:05")]
        public void Operator_Divider_Time(string timeString, double divider, string expectedTimeString)
        {
            Time time = new Time(timeString);

            Time expectedTime = new Time(expectedTimeString);

            Assert.IsTrue(time / divider == expectedTime);
        }
    }

    [TestClass]
    public class TimeUnitTestsMethods
    {
        [DataTestMethod, TestCategory("Methods")]
        [DataRow((byte)12, (byte)34, (byte)56, "12:34:56")]
        [DataRow((byte)09, (byte)12, (byte)34, "09:12:34")]
        [DataRow((byte)00, (byte)00, (byte)00, "00:00:00")]
        [DataRow((byte)00, (byte)00, null, "00:00:00")]
        [DataRow((byte)00, null, null, "00:00:00")]
        [DataRow(null, null, null, "00:00:00")]
        public void ToString_Override(byte hours, byte minutes, byte seconds, string expectedString)
        {
            Time time = new Time(hours, minutes, seconds);

            Assert.AreEqual(time.ToString(), expectedString);
            Assert.AreEqual($"{time}", expectedString);
        }
    }
}