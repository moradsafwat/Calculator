using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Calculator("(1+(4+5+2)-3)+(6+8)"));
        }

        public static int Calculator(string s)
        {
            char[] expressionString = s.ToCharArray();
            Stack<int> number = new Stack<int>();
            Stack<char> Operator = new Stack<char>();

            for (int i = 0; i < expressionString.Length; i++)
            {
                if (expressionString[i] == ' ')
                {
                    continue;
                }

                if (expressionString[i] >= '0' && expressionString[i] <= '9')
                {
                    StringBuilder variable = new StringBuilder();

                    while (i < expressionString.Length && expressionString[i] >= '0' && expressionString[i] <= '9')
                    {
                        variable.Append(expressionString[i++]);
                    }
                    number.Push(int.Parse(variable.ToString()));
                    i--;
                }
                else if (expressionString[i] == '(')
                {
                    Operator.Push(expressionString[i]);
                }
                else if (expressionString[i] == ')')
                {
                    while (Operator.Peek() != '(')
                    {
                        number.Push(applyOperator(Operator.Pop(), number.Pop(), number.Pop()));
                    }
                    Operator.Pop();
                }
                else if (expressionString[i] == '+' ||
                         expressionString[i] == '-' ||
                         expressionString[i] == '*' ||
                         expressionString[i] == '/')
                {
                    while (Operator.Count > 0 && hasPrecedence(expressionString[i], Operator.Peek()))
                    {
                        number.Push(applyOperator(Operator.Pop(), number.Pop(), number.Pop()));
                    }
                    Operator.Push(expressionString[i]);
                }
            }
            while (Operator.Count > 0)
            {
                number.Push(applyOperator(Operator.Pop(), number.Pop(), number.Pop()));
            }
            return number.Pop();
        }
        public static bool hasPrecedence(char operator1, char operator2)
        {
            if (operator2 == '(' || operator2 == ')')
            {
                return false;
            }
            if ((operator1 == '*' || operator1 == '/') && (operator2 == '+' || operator2 == '-'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static int applyOperator(char operators, int b, int a)
        {
            switch (operators)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    if (b == 0)
                    {
                        Console.WriteLine("Cannot divide by zero");
                        break;
                    }
                    return a / b;
            }
            return 0;
        }

    }
}