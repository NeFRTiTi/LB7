using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fractions
{
    public class Fraction : IComparable<Fraction>, IEquatable<Fraction>
    {
        // Numerator
        public readonly int Num;
        // Denominator
        public readonly int Den;

        public Fraction(int num, int den)
        {
            if (den == 0)
                throw new DivideByZeroException();

            Num = num;
            Den = den;
        }
        

        //Получаем по строковому представлению
        public Fraction(string s)
        {
            if (!s.Contains('/') || s.Count(c => c == '/') != 1)
                throw new ArithmeticException("Not a rational number");
            string[] result = s.Split('/');
            foreach (var v in result)
            {
                //all проверяет что каждый символ строки вернет true на каждый символ
                if (!v.All(char.IsDigit))
                    throw new ArithmeticException("Not a rational number");
            }
            this.Num = int.Parse(result[0]);
            this.Den = int.Parse(result[1]);
        }
        
        
        //Если число / число
        //Получаем по строковому представлению через регулярку
        public static Fraction fromString(string s)
        {
            Regex re = new Regex(@"(?<Num>\d+)\/(?<Den>\d+)");
            if (!re.IsMatch(s))
                throw new ArithmeticException();
            Match m = re.Match(s);
            
            int num = int.Parse(m.Groups["Num"].Value);
            int den = int.Parse(m.Groups["Den"].Value);
            
            return new Fraction(num, den);  
        }
        //Если int, вызываем int и т.д
        public static explicit operator int(Fraction a) => a.AsInt;
        public static explicit operator float(Fraction a) => a.AsFloat; //ЧТобы выводилось строковое представление, а не float
        public static explicit operator decimal(Fraction a) => a.AsDecimal;

        public float AsFloat
        {
            get => (float)Num / Den;
        }
        public decimal AsDecimal
        {
            get => (decimal)Num / Den;
        }

        public int AsInt
        {
            get => Num / Den;
        }
        
        public override string ToString() => $"{Num}/{Den}";

        //позволяет форматировать строку в зависимости от интернализации
        public string ToString(IFormatProvider fmt) => $"{Num.ToString(fmt)}/{Den.ToString(fmt)}";
         
        //Проверка двух объектов на равенство от IEquitable 
        public bool Equals(Fraction other)
        {
            return Num == other.Num && Den == other.Den;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Fraction) obj);
        }

        //Те объекты которые не имеют хешкодов нельзя использовать в словарях
        public override int GetHashCode()
        {
            return HashCode.Combine(Num, Den);
        }

        //Порядковое сравнение от IComparable
        public int CompareTo(Fraction? obj)
        {
            if (obj == null) return 1;

            return this.AsDecimal.CompareTo(obj.AsDecimal);
        }

        //явно ставим + и -
        //Математические операции
        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(-a.Num, a.Den);
        // дроби
        public static Fraction operator +(Fraction a, Fraction b) =>
            new Fraction(a.Num * b.Den + b.Num * a.Den, a.Den * b.Den);        
        public static Fraction operator -(Fraction a, Fraction b) => a + (-b);
        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.Num * b.Num, a.Den * b.Den);
        public static Fraction operator *(Fraction a, int b) => new Fraction(a.Num * b, a.Den);
        
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Num == 0)
                throw new DivideByZeroException();

            return new Fraction(a.Num * b.Den, a.Den * b.Num);
        }
        public static Fraction operator /(Fraction a, int b) => new Fraction(a.Num, a.Den * b);
        
        //Математические функции сравнения
        public static bool operator !=(Fraction a, Fraction b) => a.AsDecimal != b.AsDecimal;
        public static bool operator ==(Fraction a, Fraction b) => !(a != b);
        public static bool operator <(Fraction a, Fraction b) => a.AsDecimal < b.AsDecimal;
        public static bool operator >(Fraction a, Fraction b) => !(a < b);
        public static bool operator <=(Fraction a, Fraction b) => a.AsDecimal <= b.AsDecimal;
        public static bool operator >=(Fraction a, Fraction b) => a.AsDecimal >= b.AsDecimal;
    }
}