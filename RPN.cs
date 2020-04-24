using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;




namespace Programowanie
{
    public class RPN
    {
  

        List<string> list = new List<string>();
        double xVar, min, max;
        int n, negative;

        
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
    
     Regex rg = new Regex(@"\.");
            this.equ = rg.Replace(this.equ,",");

            if(this.equ[0]=='-' && this.equ[1]!='(') 
            {
                this.equ = "0"+this.equ;
            }
            else if(this.equ[0]=='-' && this.equ[1]=='(') 
            {
                this.negative = 1;
                this.equ = this.equ.Remove(0,1);
            }
}

public RPN (string equ, double x)
{
    this.equ = equ; 
    this.xVar = x;
    
     Regex rg = new Regex(@"\.");
            this.equ = rg.Replace(this.equ,",");

            if(this.equ[0]=='-' && this.equ[1]!='(') 
            {
                this.equ = "0"+this.equ;
            }
            else if(this.equ[0]=='-' && this.equ[1]=='(') 
            {
                this.negative = 1;
                this.equ = this.equ.Remove(0,1);
            }
}
 public RPN (string equ, double x_min, double  x_max, int n)
        {
         this.equ = equ;
         this.min = x_min;
         this.max = x_max;
         this.n = n;


        }
         public RPN (string equ, double x, double x_min, double  x_max, int n)
        {
         double tmp;
           
         this.xVar = x;
         this.min = x_min;
         this.max = x_max;
         this.n = n;


         Regex rg = new Regex(@"\.");
            this.equ = rg.Replace(this.equ,",");

            if(this.equ[0]=='-' && this.equ[1]!='(') 
            {
                this.equ = "0"+this.equ;
            }
            else if(this.equ[0]=='-' && this.equ[1]=='(') 
            {
                this.negative = 1;
                this.equ = this.equ.Remove(0,1);
            }


          if(min>max)
            {
                tmp = this.min;
                this.min = this.max;
                this.max = tmp;
            }
            


        }
        
        

        public string[] TokensToArray()
        {
            
            int i=0;
            Regex rx = new Regex(@"\(|\)|\^|\*|\/|\+|\-|(abs)|(cos)|(exp)|(log)|(sin)|(sqrt)|(tan)|(cosh)|(sinh)|(tanh)|(acos)|(asin)|(atan)|(x)|((\d*)(\,)?(\d+))");
            MatchCollection tokens = rx.Matches(this.equ);
            string[] ArrayOfTokens = new string[tokens.Count];
            foreach (Match token in tokens)
        {                        
            ArrayOfTokens[i] = token.Value; 
            i++;
            
        }           

            return ArrayOfTokens;
        }
       
             public bool Valid()
        {
            int bracket = 0;
            string[] token= new string[this.TokensToArray().Length];
            token = this.TokensToArray();
            
            

            for(int i = 0; i < token.Length; i++)
            {
                if(pd.ContainsKey(token[i]))
                {
                    if(pd.ContainsKey(token[i+1]) && (pd[token[i]]==1 && pd[token[i]]==2 && pd[token[i]]==3) && (pd[token[i+1]]==1 && pd[token[i+1]]==2 && pd[token[i+1]]==3))
                    {
                        Console.WriteLine("incorrect math operators");
                        return false;
                    }
                    else if(pd[token[i]]==4 && token[i+1]!="(")
                    {
                        Console.WriteLine("incorrect math function usage");
                        return false;
                    }
                    
                }

                if(token[i]=="(") 
                {
                   bracket++;
                }
                else if(token[i]==")") 
                {
                    bracket--;
                }
                
            }

            if(bracket!=0){
                Console.WriteLine("incorrect bracket quantity");
                return false;
            }

            return true;
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
            while(S.Count>0)Q.Enqueue(S.Pop());
                    
            foreach(string a in Q.ToArray())
            {
            list.Add(a);
            
            }
            Console.WriteLine();
            return Q.ToArray();
             
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
                    double a =0;
                    if(S1.Count>0)a = S1.Pop();
                    else{
                        Console.WriteLine("wrong equation format");
                        Environment.Exit(0);

                    }
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
                        double b =0;
                        if(S1.Count>0){ b = S1.Pop();}
                        else{
                            Console.WriteLine("wrong equation format");
                            Environment.Exit(0);

                        }
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
                if(negative ==1)return S1.Pop()*(-1);
                else{
                    return S1.Pop();
                }
            }
            else return 0;
        }

        public object[] returnValues()
        {
            object[] resultTable = new object[n];
            
            double delta = (this.max - this.min)/(n-1);
            
            xVar = min;

            for(int i = 0;  i<= n-1; i++)
            {
                resultTable[i] = new{
                    x = xVar, y = returnValue()
                };
                xVar+=delta;
            }
            return resultTable;
                        
        }
     
    }

}