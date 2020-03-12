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
            //obj.temp();
            obj.getPostfix();
            Console.WriteLine("wynika dla x: {0}",obj.returnValue());
            obj.returnValueNotOnce();
           
        }
    }
}
