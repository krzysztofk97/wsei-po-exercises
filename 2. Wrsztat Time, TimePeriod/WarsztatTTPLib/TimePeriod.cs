using System;
using WarsztatTTPLib.Enums;

namespace WarsztatTTPLib
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /// <summary>
        /// Czas podany w sekundach.
        /// </summary>
        public readonly long Period;

        /// <summary>
        /// Liczba sekund.
        /// </summary>
        public readonly int Seconds => (int)(Period % 60);

        /// <summary>
        /// Liczba minut.
        /// </summary>
        public readonly int Minutes => (int)(Period / 60 % 60);

        /// <summary>
        /// Liczba godzin.
        /// </summary>
        public readonly int Hours => (int)(Period / 3600);

        /// <summary>
        /// Konstruktor klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="period">Czas w formie liczby sekund</param>
        public TimePeriod(long period)
        {
            if (period < 0)
                throw new ArgumentOutOfRangeException("Seconds must be a not negative number");

            Period = period;
        }

        /// <summary>
        /// Konstruktor klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="hours">Liczba godzin</param>
        /// <param name="minutes">Liczba minut</param>
        /// <param name="seconds">Liczba sekund</param>
        public TimePeriod(int hours = 0, int minutes = 0, int seconds = 0) => Period = ValidateTimePart(hours, TimePart.hours) * 3600 + ValidateTimePart(minutes, TimePart.minutes) * 60 + ValidateTimePart(seconds, TimePart.seconds);

        /// <summary>
        /// Konstruktor klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriodString">Czas podany w formacie: hh:mm:ss</param>
        public TimePeriod(string timePeriodString) => Period = ParseTimePeriod(timePeriodString);

        /// <summary>
        /// Standardowa reprezentacja tekstowa czasu w formacie hh:mm:ss.
        /// </summary>
        /// <returns>Wartość typu <c>string</c> w formacie hh:mm:ss</returns>
        public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

        public static implicit operator TimePeriod(string timePeriodString) => new TimePeriod(timePeriodString);

        public bool Equals(TimePeriod other)
        {
            if(other.Period == Period)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if(obj is TimePeriod)
                return this.Equals((TimePeriod)obj);
            else
                return false;
        }

        public override int GetHashCode() => Period.GetHashCode();

        public int CompareTo(TimePeriod other) => this.Period.CompareTo(other.Period);

        /// <summary>
        /// Operator <c>==</c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekty są równe lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator == (TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1.Equals(timePeriod2);
        
        /// <summary>
        /// Operator <c>!=</c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekty nie są równe lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator != (TimePeriod timePeriod1, TimePeriod timePeriod2) => !timePeriod1.Equals(timePeriod2);

        /// <summary>
        /// Operator <c>></c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>timePeriod1</c> jest większy od obiektu <c>timePeriod2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator > (TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1.CompareTo(timePeriod2) == 1;

        /// <summary>
        /// Operator <c><</c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>timePeriod1</c> jest mniejszy od obiektu <c>timePeriod2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator < (TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1.CompareTo(timePeriod2) == -1;

        /// <summary>
        /// Operator <c>>=</c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>timePeriod1</c> jest większy bądź równy obiektowi <c>timePeriod2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator >= (TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1.CompareTo(timePeriod2) >= 0;

        /// <summary>
        /// Operator <c><=</c> porównujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <param name="timePeriod2">Porównywany obiekt klasy <c>TimePeriod</c></param>
        /// <returns><c>true</c> w przypadku kiedy obiekt <c>timePeriod1</c> jest mniejszy bądź równy obiektowi <c>timePeriod2</c> lub <c>false</c> w przeciwnym przypadku</returns>
        public static bool operator <= (TimePeriod timePeriod1, TimePeriod timePeriod2) => timePeriod1.CompareTo(timePeriod2) <= 0;

        /// <summary>
        /// Operator <c>+</c> sumujący dwa obiekty klasy <c>TimePeriod</c>.
        /// </summary>
        /// <param name="timePeriod1">Składnik działania</param>
        /// <param name="timePeriod2">Składnik działania</param>
        /// <returns>Wynik sumowania dwóch obiektów klasy <c>TimePeriod</c></returns>
        public static TimePeriod operator + (TimePeriod timePeriod1, TimePeriod timePeriod2) => new TimePeriod(timePeriod1.Period + timePeriod2.Period);

        /// <summary>
        /// Operator <c>-</c> odejmujący obiekt klasy <c>TimePeriod</c> od drugiego obiektu tej samej klasy.
        /// </summary>
        /// <param name="timePeriod1">Odjemna</param>
        /// <param name="timePeriod2">Odjemnik</param>
        /// <returns>Wynik odejmowania obiektów klasy <c>TimePeriod</c></returns>
        public static TimePeriod operator - (TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            if (timePeriod1.Period < timePeriod2.Period)
                return new TimePeriod();

            return new TimePeriod(timePeriod1.Period - timePeriod2.Period);
        }

        /// <summary>
        /// Operator <c>*</c> mnożący czas zawarty w obiekcie klasy <c>TimePeriod</c>. 
        /// </summary>
        /// <param name="timePeriod">Okres do pomnożenia</param>
        /// <param name="multiplier">Mnożnik</param>
        /// <returns>Wynik mnożenia obiektu klasy <c>TimePeriod</c> przez liczbę w postaci obiektu klasy <c>TimePeriod</c></returns>
        public static TimePeriod operator * (TimePeriod timePeriod, double multiplier) => new TimePeriod((long)(multiplier * timePeriod.Period));

        /// <summary>
        /// Operator <c>*</c> mnożący czas zawarty w obiekcie klasy <c>TimePeriod</c>. 
        /// </summary>
        /// <param name="multiplier">Mnożnik</param>
        /// <param name="timePeriod">Okres do pomnożenia</param>
        /// <returns>Wynik mnożenia obiektu klasy <c>TimePeriod</c> przez liczbę w postaci obiektu klasy <c>TimePeriod</c></returns>
        public static TimePeriod operator * (double multiplier, TimePeriod timePeriod) => timePeriod * multiplier;

        /// <summary>
        /// Operator <c>/</c> dzielący czas zawarty w obiekcie klasy <c>TimePeriod</c> przez liczbę.
        /// </summary>
        /// <param name="timePeriod">Okres do podzielenia</param>
        /// <param name="divider">Dzielnik</param>
        /// <returns>Wynik dzielenia obiektu klasy <c>TimePeriod</c> przez liczbę w postaci obiektu klasy <c>TimePeriod</c></returns>
        public static TimePeriod operator / (TimePeriod timePeriod, double divider) => new TimePeriod((long)(timePeriod.Period / divider));

        private static long ParseTimePeriod(string timePeriodString)
        {
            if (timePeriodString.Trim().Length <= 0)
                throw new ArgumentException("Time period string cannot be empty");

            string[] timePeriodStringArray = timePeriodString.Split(':');
            int[] timePeriodArray = new int[3] { 0, 0, 0 };

            if (timePeriodStringArray.Length >= 1)
                if (!int.TryParse(timePeriodStringArray[0], out timePeriodArray[0]))
                    throw new ArgumentException("Hours must be a non negative number");

            if (timePeriodStringArray.Length >= 2)
                if (!int.TryParse(timePeriodStringArray[1], out timePeriodArray[1]))
                    throw new ArgumentException("Minutes must be a number between 0 and 59");

            if (timePeriodStringArray.Length == 3)
                if (!int.TryParse(timePeriodStringArray[2], out timePeriodArray[2]))
                    throw new ArgumentException("Seconds must be a number between 0 and 59");

            if (timePeriodStringArray.Length > 3)
                throw new ArgumentException("Constructor accepts only hours, minutes or seconds in format hh:mm:ss");

            timePeriodArray[0] = ValidateTimePart(timePeriodArray[0], TimePart.hours);
            timePeriodArray[1] = ValidateTimePart(timePeriodArray[1], TimePart.minutes);
            timePeriodArray[2] = ValidateTimePart(timePeriodArray[2], TimePart.seconds);

            return timePeriodArray[0] * 3600 + timePeriodArray[1] * 60 + timePeriodArray[2];
        }

        private static int ValidateTimePart(int value, TimePart timePart)
        {
            if (timePart == TimePart.hours)
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Hours must be a non negative number");

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