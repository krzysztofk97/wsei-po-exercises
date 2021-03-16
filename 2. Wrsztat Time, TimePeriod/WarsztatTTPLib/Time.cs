using System;
using WarsztatTTPLib.Enums;

namespace WarsztatTTPLib
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Liczba godzin.
        /// </summary>
        public readonly byte Hours;

        /// <summary>
        /// Liczba minut.
        /// </summary>
        public readonly byte Minutes;

        /// <summary>
        /// Liczba sekund.
        /// </summary>
        public readonly byte Seconds;

        /// <summary>
        /// Godzina wyrażona w formie ilości sekund, które upłynęły od godziny 00:00:00.
        /// </summary>
        public readonly long InSeconds => Hours * 3600 + Minutes * 60 + Seconds;

        /// <summary>
        /// Konstruktor klasy <c>Time</c>.
        /// </summary>
        /// <param name="hours">Liczba godzin</param>
        /// <param name="minutes">Liczba minut</param>
        /// <param name="seconds">Liczba sekund</param>
        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0)
        {
            Hours = ValidateTimePart(hours, TimePart.hours);
            Minutes = ValidateTimePart(minutes, TimePart.minutes);
            Seconds = ValidateTimePart(seconds, TimePart.seconds);
        }

        /// <summary>
        /// Konstruktor klasy <c>Time</c>.
        /// </summary>
        /// <param name="timeString">Godzina podana w formacie: hh:mm:ss</param>
        public Time(string timeString)
        {
            byte[] timeArray = ParseTime(timeString);

            Hours = timeArray[0];
            Minutes = timeArray[1];
            Seconds = timeArray[2];
        }

        /// <summary>
        /// Konstruktor klasy <c>Time</c>.
        /// </summary>
        /// <param name="timeInSeconds">Godzina podana w formie ilości sekund, które upłynęły od godziny 00:00:00, w zakresie od 0 do 86399 sekund</param>
        public Time(int timeInSeconds)
        {
            if (timeInSeconds < 0 || timeInSeconds > 86399)
                throw new ArgumentOutOfRangeException("Time provided in seconds must be a number between 0 and 86399");

            Seconds = (byte)(timeInSeconds % 60);
            Minutes = (byte)(timeInSeconds / 60 % 60);
            Hours = (byte)(timeInSeconds / 3600);
        }

        /// <summary>
        /// Standardowa reprezentacja tekstowa godziny w formacie hh:mm:ss.
        /// </summary>
        /// <returns>Wartość typu <c>string</c> w formacie hh:mm:ss</returns>
        public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

        public static implicit operator Time(string timeString) => new Time(timeString);

        public bool Equals(Time other)
        {
            if (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return Equals(obj);
        }

        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();

        public int CompareTo(Time other) => (Seconds + Minutes * 100 + Hours * 10000).CompareTo(other.Seconds + other.Minutes * 100 + other.Hours * 10000);

        /// <summary>
        /// Operator <c>==</c> porównujący dwa obiekty klasy <c>Time</c>.
        /// </summary>
        /// <param name="time1">Porównywany obiekt klasy <c>Time</c></param>
        /// <param name="time2">Porównywany obiekt klasy <c>Time</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekty są równe lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator == (Time time1, Time time2) => time1.Equals(time2);

        /// <summary>
        /// Operator <c>!=</c> porównujący dwa obiekty klasy <c>Time</c>.
        /// </summary>
        /// <param name="time1">Porównywany obiekt klasy <c>Time</c></param>
        /// <param name="time2">Porównywany obiekt klasy <c>Time</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekty nie są równe lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator != (Time time1, Time time2) => !time1.Equals(time2);

        /// <summary>
        /// Operator <c>></c> porównujący dwa obiekty klasy <c>Time</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>Time</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>Time</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>time1</c> jest większy od obiektu <c>time2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator > (Time time1, Time time2) => time1.CompareTo(time2) == 1;

        /// <summary>
        /// Operator <c><</c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>timePeriod1</c> jest mniejszy od obiektu <c>timePeriod2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator < (Time time1, Time time2) => time1.CompareTo(time2) == -1;

        /// <summary>
        /// Operator <c>>=</c> porównujący dwa obiekty klasy <c>Time</c>.
        /// </summary>
        /// <param name="time1">Porównywany obiekt klasy <c>Time</c></param>
        /// <param name="time2">Porównywany obiekt klasy <c>Time</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>time1</c> jest większy bądź równy obiektowi <c>time2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator >= (Time time1, Time time2) => time1.CompareTo(time2) >= 0;

        /// <summary>
        /// Operator <c><=</c> porównujący dwa obiekty klasy <c>Time</c>.
        /// </summary>
        /// <param name="time1">Porównywany obiekt klasy <c>Time</c></param>
        /// <param name="time2">Porównywany obiekt klasy <c>Time</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>time1</c> jest mniejszy bądź równy obiektowi <c>time2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator <= (Time time1, Time time2) => time1.CompareTo(time2) <= 0;

        /// <summary>
        /// Operator <c>+</c> sumujący dwa obiekty klasy <c>Time</c>.
        /// </summary>
        /// <param name="time1">Składnik działania</param>
        /// <param name="time2">Składnik działania</param>
        /// <returns>Wynik sumowania dwóch obiektów klasy <c>Time</c></returns>
        public static Time operator + (Time time1, Time time2) => new Time((int)((time1.InSeconds + time2.InSeconds) % 86400));

        /// <summary>
        /// Operator <c>+</c> sumujący dwa obiekt klasy <c>TimePeriod</c> oraz obiekt klasy <c>Time</c>.
        /// </summary>
        /// <param name="time">Składnik <c>Time</c> działania</param>
        /// <param name="timePeriod">Składnik <c>TimePeriod</c> działania</param>
        /// <returns>Wynik sumowania obiektu klasy <c>TimePeriod</c> oraz obiektu klasy <c>Time</c> w postaci obiektu klasy <c>Time</c></returns>
        public static Time operator + (Time time, TimePeriod timePeriod) => new Time((int)((time.InSeconds + timePeriod.Period % 86400) % 86400));

        /// <summary>
        /// Operator <c>-</c> odejmujący obiekt klasy <c>Time</c> od drugiego obiektu tej samej klasy.
        /// </summary>
        /// <param name="time1">Odjemna</param>
        /// <param name="time2">Odjemnik</param>
        /// <returns>Wynik odejmowania obiektów klasy <c>Time</c></returns>
        public static Time operator - (Time time1, Time time2) => new Time((int)(time1.InSeconds + 86400 - time2.InSeconds) % 86400);

        /// <summary>
        /// Operator <c>-</c> odejmujący obiekt klasy <c>TimePeriod</c> od obiektu klasy <c>Time</c>.
        /// </summary>
        /// <param name="time">Składnik <c>Time</c> działania</param>
        /// <param name="timePeriod">Składnik <c>TimePeriod</c> działania</param>
        /// <returns>Wynik odejmowania obiektów klasy <c>TimePeriod</c> oraz <c>Time</c> w postaci <c>Time</c></returns>
        public static Time operator - (Time time, TimePeriod timePeriod) => new Time((int)(time.InSeconds + 86400 - timePeriod.Period % 86400) % 86400);

        /// <summary>
        /// Operator <c>*</c> mnożący godzinę zawartą w obiekcie klasy <c>Time</c>. 
        /// </summary>
        /// <param name="time">Czas do pomnożenia</param>
        /// <param name="multiplier">Mnożnik</param>
        /// <returns>Wynik mnożenia obiektu klasy <c>Time</c> przez liczbę w postaci obiektu klasy <c>Time</c></returns>
        public static Time operator * (Time time, double multiplier) => new Time((int)(time.InSeconds * multiplier % 86400));

        /// <summary>
        /// Operator <c>*</c> mnożący godzinę zawartą w obiekcie klasy <c>Time</c>. 
        /// </summary>
        /// <param name="multiplier">Mnożnik</param>
        /// <param name="time">Czas do pomnożenia</param>
        /// <returns>Wynik mnożenia obiektu klasy <c>Time</c> przez liczbę w postaci obiektu klasy <c>Time</c></returns>
        public static Time operator * (double multiplier, Time time) => time * multiplier;

        /// <summary>
        /// Operator <c>/</c> dzielący godzinę zawartą w obiekcie klasy <c>Time</c> przez liczbę.
        /// </summary>
        /// <param name="time">Czas do podzielenia</param>
        /// <param name="divider">Dzielnik</param>
        /// <returns>Wynik dzielenia obiektu klasy <c>Time</c> przez liczbę w postaci obiektu klasy <c>Time</c></returns>
        public static Time operator / (Time time, double divider) => new Time((int)(time.InSeconds / divider % 86400));

        private static byte[] ParseTime(string timeString)
        {
            if (timeString.Trim().Length <= 0)
                throw new ArgumentException("Time string cannot be empty");

            string[] timeStringArray = timeString.Split(':');
            byte[] timeArray = new byte[3] { 0, 0, 0 };

            if (timeStringArray.Length >= 1)
                if (!byte.TryParse(timeStringArray[0], out timeArray[0]))
                    throw new ArgumentException("Hours must be a number between 0 and 23");

            if (timeStringArray.Length >= 2)
                if (!byte.TryParse(timeStringArray[1], out timeArray[1]))
                    throw new ArgumentException("Minutes must be a number between 0 and 59");

            if (timeStringArray.Length == 3)
                if (!byte.TryParse(timeStringArray[2], out timeArray[2]))
                    throw new ArgumentException("Seconds must be a number between 0 and 59");

            if (timeStringArray.Length > 3)
                throw new ArgumentException("Constructor accepts only hours, minutes or seconds in format hh:mm:ss");

            timeArray[0] = ValidateTimePart(timeArray[0], TimePart.hours);
            timeArray[1] = ValidateTimePart(timeArray[1], TimePart.minutes);
            timeArray[2] = ValidateTimePart(timeArray[2], TimePart.seconds);

            return timeArray;
        }

        private static byte ValidateTimePart(byte value, TimePart timePart)
        {
            if (timePart == TimePart.hours)
                if (value < 0 || value > 23)
                    throw new ArgumentOutOfRangeException("Hours must be a number between 0 and 23");

            if (timePart == TimePart.minutes)
                if (value < 0 || value > 59)
                    throw new ArgumentOutOfRangeException("Minutes must be a number between 0 and 59");

            if (timePart == TimePart.seconds)
                if (value < 0 || value > 59)
                    throw new ArgumentOutOfRangeException("Seconds must be a number between 0 and 59");

            return value;
        }
    }
}