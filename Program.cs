using System;
using System.Globalization;

namespace Programowanie
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 5)
            {
                Console.WriteLine("not enough arg");
                Environment.Exit(0);
            }

            string equ;
            double x;
            int  x_min, x_max, n;
            equ = args[0];
            
            if (!double.TryParse(args[1], System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out x) &&
                !double.TryParse(args[1], System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out x) &&
                !double.TryParse(args[1], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out x))
                {
                    Console.WriteLine("wrong format");
                    Environment.Exit(0);
                }

            
            x_min = int.Parse(args[2]);
            x_max = int.Parse(args[3]);
            n = int.Parse(args[4]);

            RPN obj = new RPN(equ,x,x_min,x_max,n);
            obj.getPostfix();
            if(obj.isCorrect())
            {


            }
            else
            {
                Console.WriteLine("incorrect equation");
                Environment.Exit(0);

            }
            Console.WriteLine(obj.returnValue());
            obj.returnValueNotOnce();
           
        }
    }
}
