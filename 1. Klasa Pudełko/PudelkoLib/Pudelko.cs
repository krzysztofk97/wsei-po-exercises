using PudelkoLib.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace PudelkoLib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        public readonly double A;
        public readonly double B;
        public readonly double C;

        private double[] Dimensions => new double[3] { A, B, C };

        public double Objetosc => Math.Round(A * B * C, 9);

        public double Pole => Math.Round(2 * (A * B + A * C + B * C), 6);

        private double ValidateBoxDimension(double dimension)
        {
            if (Math.Round(dimension, 3) <= 0)
                throw new ArgumentOutOfRangeException("Wymiary pudełka muszą być dodatnie");

            if (Math.Round(dimension, 3) > 10)
                throw new ArgumentOutOfRangeException("Pojedynczy wymiar pudełka nie może być większy niż 10 m");

            return dimension;
        }

        private double ConvertUnitOfMeasure(double dimension, UnitOfMeasure inputUnit, UnitOfMeasure outputUnit = UnitOfMeasure.meter)
        {
            if (inputUnit == outputUnit)
                return dimension;

            switch (inputUnit)
            {
                case UnitOfMeasure.centimeter:
                    dimension = dimension / 100;
                    break;

                case UnitOfMeasure.milimeter:
                    dimension = dimension / 1000;
                    break;

                default:
                    break;
            }

            return outputUnit switch
            {
                UnitOfMeasure.centimeter => dimension * 100,
                UnitOfMeasure.milimeter => dimension * 1000,
                _ => dimension
            };
        }

        public Pudelko()
        {
            A = 0.1d;
            B = 0.1d;
            C = 0.1d;
        }

        public Pudelko(double a, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            A = ValidateBoxDimension(ConvertUnitOfMeasure(a, unit));
            B = 0.1d;
            C = 0.1d;
        }

        public Pudelko(double a, double b, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            A = ValidateBoxDimension(ConvertUnitOfMeasure(a, unit));
            B = ValidateBoxDimension(ConvertUnitOfMeasure(b, unit));
            C = 0.1d;
        }

        public Pudelko(double a, double b, double c, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            A = ValidateBoxDimension(ConvertUnitOfMeasure(a, unit));
            B = ValidateBoxDimension(ConvertUnitOfMeasure(b, unit));
            C = ValidateBoxDimension(ConvertUnitOfMeasure(c, unit));
        }

        public override string ToString()
        {
            return this.ToString("m", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            if (String.IsNullOrEmpty(format)) format = "m";
            if (provider == null) provider = CultureInfo.InvariantCulture;

            return (format != null ? format.ToLower() : null) switch
            {
                "m" => string.Format($"{string.Format(provider, "{0:0.000}", A)} m \u00d7 {string.Format(provider, "{0:0.000}", B)} m \u00d7 {string.Format(provider, "{0:0.000}", C)} m"),
                "cm" => string.Format($"{string.Format(provider, "{0:0.0}", ConvertUnitOfMeasure(A, UnitOfMeasure.meter, UnitOfMeasure.centimeter))} cm \u00d7 {string.Format(provider, "{0:0.0}", ConvertUnitOfMeasure(B, UnitOfMeasure.meter, UnitOfMeasure.centimeter))} cm \u00d7 {string.Format(provider, "{0:0.0}", ConvertUnitOfMeasure(C, UnitOfMeasure.meter, UnitOfMeasure.centimeter))} cm"),
                "mm" => string.Format($"{string.Format(provider, "{0:0}", ConvertUnitOfMeasure(A, UnitOfMeasure.meter, UnitOfMeasure.milimeter))} mm \u00d7 {string.Format(provider, "{0:0}", ConvertUnitOfMeasure(B, UnitOfMeasure.meter, UnitOfMeasure.milimeter))} mm \u00d7 {string.Format(provider, "{0:0}", ConvertUnitOfMeasure(C, UnitOfMeasure.meter, UnitOfMeasure.milimeter))} mm"),
                _ => throw new FormatException()
            };
        }

        public bool Equals(Pudelko other)
        {
            double[] thisDimensions = Dimensions;
            double[] otherDimensions = other.Dimensions;

            Array.Sort(thisDimensions);
            Array.Sort(otherDimensions);

            if (other == null)
                return false;

            for(int i = 0; i < 3; i++)
                if(thisDimensions[i] != otherDimensions[i])
                    return false;

            return true;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Pudelko pudelkoObj = obj as Pudelko;

            if (pudelkoObj == null)
                return false;
            else
                return Equals(pudelkoObj);
        }

        public override int GetHashCode() => (A, B, C).GetHashCode();

        public IEnumerator<double> GetEnumerator()
        {
            foreach(double dimension in Dimensions)
                yield return dimension;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public static bool operator == (Pudelko pudelko1, Pudelko pudelko2)
        {
            if (((object)pudelko1) == null || ((object)pudelko2) == null)
                return Object.Equals(pudelko1, pudelko2);

            return pudelko1.Equals(pudelko2);
        }

        public static bool operator != (Pudelko pudelko1, Pudelko pudelko2)
        {
            if (((object)pudelko1) == null || ((object)pudelko2) == null)
                return ! Object.Equals(pudelko1, pudelko2);

            return ! pudelko1.Equals(pudelko2);
        }

        public static implicit operator Pudelko(ValueTuple<double, double, double> vt) => new Pudelko(vt.Item1, vt.Item2, vt.Item3, UnitOfMeasure.milimeter);

        public static explicit operator double[](Pudelko pudelko) => pudelko.Dimensions;

        public double this[int dimension] => Dimensions[dimension];

        public static Pudelko Parse(string s)
        { 
            string[] splitedString = s.Trim().Split(" ");

            if (splitedString.Length != 8)
                throw new FormatException();

            UnitOfMeasure unit;

            double a;

            if (double.TryParse(splitedString[0], out a) == false)
                throw new FormatException();
            
            double b;

            if (double.TryParse(splitedString[3], out b) == false)
                throw new FormatException();
            
            double c;

            if (double.TryParse(splitedString[6], out c) == false)
                throw new FormatException();

            switch (splitedString[1].ToLower())
            {
                case "m":
                    unit = UnitOfMeasure.meter;
                    break;

                case "cm":
                    unit = UnitOfMeasure.centimeter;
                    break;

                case "mm":
                    unit = UnitOfMeasure.milimeter;
                    break;

                default:
                    throw new FormatException();
            }

            return new Pudelko(a, b, c, unit);
        }
    }
}