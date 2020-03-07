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
            RPN obj = new RPN(equ);
            Console.WriteLine(obj.temp());
           
        }
    }
}
