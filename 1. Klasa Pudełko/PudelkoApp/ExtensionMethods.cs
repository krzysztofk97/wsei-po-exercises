using System;
using PudelkoLib;

namespace PudelkoApp
{
    public static class ExtensionMethods
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            double a = Math.Pow(p.Objetosc, 1d / 3d);
            return new Pudelko(a, a, a);
        }
    }
}
