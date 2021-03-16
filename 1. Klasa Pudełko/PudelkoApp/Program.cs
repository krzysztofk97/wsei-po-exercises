using System;
using System.Collections.Generic;
using System.Linq;
using PudelkoLib;
using PudelkoLib.Enums;

namespace PudelkoApp
{
    class Program
    {
        private static int ComparePudelka(Pudelko pudelko1, Pudelko pudelko2)
        {
            if (pudelko1.Objetosc == pudelko2.Objetosc)
            {
                if (pudelko1.Pole == pudelko2.Pole)
                {
                    return pudelko1.Sum().CompareTo(pudelko2.Sum());
                }

                else return pudelko1.Pole.CompareTo(pudelko2.Pole);
            }

            else return pudelko1.Objetosc.CompareTo(pudelko2.Objetosc);
        }

        static void Main(string[] args)
        {
            ValueTuple<int, int, int> pudelkoValueTuple = new ValueTuple<int, int, int>(1000, 2000, 300);

            List<Pudelko> pudelka = new List<Pudelko>
            {
                new Pudelko(),
                new Pudelko(3, unit: UnitOfMeasure.centimeter),
                new Pudelko(790, 120, unit: UnitOfMeasure.milimeter),
                new Pudelko(2.6, 1.7, 9.0),
                new Pudelko(0.21),
                new Pudelko(90, 800, 210, unit: UnitOfMeasure.milimeter),
                Pudelko.Parse("2,500 m × 9,321 m × 0,100 m"),
                pudelkoValueTuple,
                new Pudelko()
            };

            Console.WriteLine("List pudełek:");

            foreach (Pudelko pudelko in pudelka)
                Console.WriteLine(pudelko);

            Comparison<Pudelko> pudelkaComparer = new Comparison<Pudelko>(ComparePudelka);

            pudelka.Sort(pudelkaComparer);

            Console.WriteLine("\nPosortowana lista pudełek:");

            foreach (Pudelko pudelko in pudelka)
                Console.WriteLine(pudelko);
            
            Console.WriteLine("\nLista objętości oraz pól całkowitych:");

            foreach (Pudelko pudelko in pudelka)
                Console.WriteLine($"{pudelko:mm} - Objętość: {pudelko.Objetosc} m\u00b3 - Pole całkowite: {pudelko.Pole} m\u00b2");

            if(pudelka[0] != pudelka.Last())
                Console.WriteLine($"\nPudełka {pudelka[0]} i {pudelka.Last():cm} nie są równe");
            
            if(pudelka[1] == pudelka[2])
                Console.WriteLine($"Pudełka {pudelka[1]} i {pudelka[2]:cm} są równe");

            Console.WriteLine($"\nPudełko {pudelka[5]:mm} po skompresowaniu ma wymiary: {pudelka[5].Kompresuj():mm}");

            Console.ReadKey();
        }
    }
}