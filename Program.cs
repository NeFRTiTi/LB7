using System;
using System.Globalization;

namespace Fractions
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction f = new Fraction(13, 15);
            Fraction d = new Fraction("25/37");
            Fraction g = new Fraction(144, 13);
            Fraction k = Fraction.fromString("24/79"); // Regular Expressions
            Console.WriteLine($"{f} {f.AsFloat} {f.AsInt}");
            Console.WriteLine($"{d} {d.AsFloat} {d.AsInt}");
            Console.WriteLine($"{g} {g.AsFloat} {g.AsInt}");
            Console.WriteLine($"{k} {k.AsFloat} {k.AsInt}");
            Console.WriteLine(f.Equals(d));
            Console.WriteLine(f.Equals(f));
            Console.WriteLine(f + d);
            Console.WriteLine(f - d);
            Console.WriteLine(f * d);
            Console.WriteLine(f / d);
            Console.WriteLine(f == d);
            Console.WriteLine(f != d);
            Console.WriteLine(f > d);
            Console.WriteLine(f < d);
            Console.WriteLine($"{f.ToString(new CultureInfo("en-US"))}");
            Console.WriteLine($"{f.ToString(new CultureInfo("ru-RU"))}");
        }
    }
}