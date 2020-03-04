using System;

namespace Programowanie
{
    class Program
    {
        static void Main(string[] args)
        {
            string equ;
            Console.Write("podaj rownanie: ");
            equ = Console.ReadLine();
            var a = new RPN(equ);
            Console.WriteLine(a.wypisz());
        }
    }
}
