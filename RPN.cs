using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;


namespace Programowanie
{
    public class RPN
    {
        List<string>list = new List<string>();
        double min,max,xVar;
        int n;
        string[] toValue = new string[1];
        
        static Dictionary<string,int> pd = new Dictionary<string, int>()
        {
            {"abs",4},{"cos",4},{"exp",4},{"log",4},{"sin",4},{"sqrt",4},{"tan",4},{"tanh",4},{"acos",4},{"asin",4},{"atan",4},
            {"^",3},
            {"*",2},{"/",2},
            {"+",1},{"-",1},
            {"(",0}
        };
        public Stack<string> S = new Stack<string>();
        public Queue<string> Q = new Queue<string>();
        string equ;

         public RPN (string equ)
        {
         this.equ = equ;   
        }
        
        
        public string[] TokensToArray()
        {
            string stringToCheck = "x";
            int i=0;
            Regex rx = new Regex(@"\(|\)|\^|\*|\/|\+|\-|(abs)|(cos)|(exp)|(log)|(sin)|(sqrt)|(tan)|(cosh)|(sinh)|(tanh)|(acos)|(asin)|(atan)|(x)|((\d*)(\.)?(\d+))");
            MatchCollection tokens = rx.Matches(this.equ);
            string[] ArrayOfTokens = new string[tokens.Count];
            foreach (Match token in tokens)
        {                        
            ArrayOfTokens[i] = token.Value; 
            i++;
        }           
        if(ArrayOfTokens.Any(stringToCheck.Contains))gimmeX();
          
            return ArrayOfTokens;
        }
        public void gimmeX()
        {
            Console.WriteLine();
            Console.Write("podaj x: ");
            if( double.TryParse(Console.ReadLine(), out xVar))gimmeMinMax();
            else gimmeX();
        }
        public void gimmeMinMax()
        {
            double temp;
            Console.WriteLine();
            Console.Write("podaj poczatek przedzialu: ");
            if(double.TryParse(Console.ReadLine(), out min))
            {
                Console.WriteLine();
                Console.Write("podaj koniec przedzialu ");
                if(double.TryParse(Console.ReadLine(), out max))
                {
                 Console.Write("\n podaj n: ");   
                 if(int.TryParse(Console.ReadLine(), out n))Console.Write("\n ok!");
                 else gimmeMinMax();

                }
                else gimmeMinMax();
            }
            else gimmeMinMax();

            if(min>max)
            {
                temp = min;
                min = max;
                max = temp;
            }
            //Console.Write("\nmin = "+ min + " max = "+ max+ "\n");
            

        }
    
        public void getPostfix()
        {
            double token1;
            foreach(string token in this.TokensToArray())
            {
                if(token=="(")S.Push(token);
                else if(token==")")
                {
                    while(S.Peek()!="(")Q.Enqueue(S.Pop());
                    //
                    S.Pop();
                }
                else if(pd.ContainsKey(token))
                {
                    while(S.Count>0 && pd[token]<=pd[S.Peek()])
                    Q.Enqueue(S.Pop());
                    S.Push(token);
                }
                else if(Double.TryParse(token, out token1) || token=="x")Q.Enqueue(token);
                
            }
            while(S.Count > 0)Q.Enqueue(S.Pop());
                    
            foreach(string a in Q.ToArray())
            list.Add(a);

            
             
        }

        
        public void temp()
        {
            foreach(string napis in this.list)
            {
                Console.WriteLine(napis);
            }
        }
        public double returnValue()
        {
            double token1;
            Stack<double> S1 = new Stack<double>();
            foreach(string token in list)
            {
                if(Double.TryParse(token, out token1))
                S1.Push(token1);
                else if(token=="x")S1.Push(xVar);
                else if(pd.ContainsKey(token))
                {
                    double a = S1.Pop();
                    if(pd[token]==4)
                    {
                        if(token=="abs") a = Math.Abs(a);
                        else if(token=="cos") a = Math.Cos(a);
                        else if(token=="exp") a = Math.Exp(a);
                        else if(token=="log") a = Math.Log(a);
                        else if(token=="sin") a = Math.Sin(a);
                        else if(token=="sqrt") a = Math.Sqrt(a);
                        else if(token=="tan") a = Math.Tan(a);
                        else if(token=="cosh") a = Math.Cosh(a);
                        else if(token=="sinh") a = Math.Sinh(a);
                        else if(token=="tanh") a = Math.Tanh(a);
                        else if(token=="acos") a = Math.Acos(a);
                        else if(token=="asin") a = Math.Asin(a);
                        else if(token=="atan") a = Math.Atan(a);
                    }
                    else
                    {
                        double b = S1.Pop();
                        if(token=="+") a += b;
                        else if(token=="-") a = b-a;
                        else if(token=="*") a *= b;
                        else if(token=="/") a = b/a;
                        else if(token=="^") a = Math.Pow(b,a);
                    }
                    S1.Push(a);
                }

            }
            if(S1.Count>0)
            {
                return S1.Pop();
            }
            else return 0;
        }

        public void returnValueNotOnce()
        {
            Console.WriteLine("dla przedzialow:");
            double diff = max - min;
            double diff2 = diff /n;
            xVar = min;

            for(int i = 0;  i<= n; i++)
            {
                Console.Write("{0}. dla x = {1} wynik to: ",i ,xVar );
                Console.WriteLine(returnValue());
                xVar+=diff2;
            }
            
            
        }
    }

}