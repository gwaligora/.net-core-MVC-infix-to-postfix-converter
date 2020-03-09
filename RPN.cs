using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;


namespace Programowanie
{
    public class RPN
    {
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
            int i=0;
            Regex rx = new Regex(@"\(|\)|\^|\*|\/|\+|\-|(abs)|(cos)|(exp)|(log)|(sin)|(sqrt)|(tan)|(cosh)|(sinh)|(tanh)|(acos)|(asin)|(atan)|(x)|((\d*)(\.)?(\d+))");
            MatchCollection tokens = rx.Matches(this.equ);
            string[] ArrayOfTokens = new string[tokens.Count];
            foreach (Match token in tokens)
        {                        
            ArrayOfTokens[i] = token.Value; 
            i++;
        }           
          //foreach(string napis in ArrayOfTokens)Console.WriteLine(napis);
            return ArrayOfTokens;
        }
        
    
        public string[] getPostfix()
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


            return Q.ToArray();
        }

        public void temp()
        {
            foreach(string napis in this.getPostfix())
            {
                Console.WriteLine(napis);
            }
        }
        public double returnValue()
        {
            double token1;
            Stack<double> S1 = new Stack<double>();
            foreach(string token in this.getPostfix())
            {
                if(Double.TryParse(token, out token1))
                S1.Push(token1);
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
            if(S1.Count>0) return S1.Pop();
            else return 0;
        }

    }

}