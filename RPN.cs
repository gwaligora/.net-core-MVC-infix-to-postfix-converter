using System;

namespace Programowanie
{
    public class RPN
    {
        public string rownanie;
         public RPN (string equation)
        {
         rownanie = equation;   
        }
        public string wypisz()
        {
            return rownanie;
        }
    }
}