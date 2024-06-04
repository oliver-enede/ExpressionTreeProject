using System;
using System.Text.RegularExpressions;

namespace ExpressionTreeProject
{
    class Program
    {
        public class Implementation
        {
            public int precedence(char op)
            {
                if (op == '*' || op == '/' || op == '%')
                    return 3;
                else if (op == '+' || op == '-')
                    return 2;
                else if (op == '^')
                    return 1;
                else
                    return -1;
            }

            public string Infix_To_Postfix(ref string expn)
            {
                Stack<char> stk = new Stack<char>();
                string output = "";
                char _out;
                foreach (var ch in expn)
                {
                    bool isAlphaBet = Regex.IsMatch(ch.ToString(), "[a-z]", RegexOptions.IgnoreCase);
                    if (Char.IsDigit(ch) || isAlphaBet)
                    {
                        output += ch;
                    }
                    else
                    {
                        switch (ch)
                        {
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '%':
                                while (stk.Count > 0 && precedence(ch) <= precedence(stk.Peek()))
                                {
                                    _out = stk.Peek();
                                    stk.Pop();
                                    output += "" + _out;
                                }
                                stk.Push(ch);
                                output += "";
                                break;
                            case '^':
                                while (stk.Count > 0 && precedence(stk.Peek()) >= 1)
                                {
                                    _out = stk.Peek();
                                    stk.Pop();
                                    output += "" + _out;
                                }
                                stk.Push(ch);
                                output += "";
                                break;
                            case '(':
                                stk.Push(ch);
                                break;
                            case ')':
                                while (stk.Count > 0 && (_out = stk.Peek()) != '(')
                                {
                                    _out = stk.Peek();
                                    stk.Pop();
                                    output += "" + _out;
                                }
                                if (stk.Count > 0 && (_out = stk.Peek()) == '(')
                                    stk.Pop();
                                break;
                        }
                    }
                }
                while (stk.Count > 0)
                {
                    _out = stk.Peek();
                    stk.Pop();
                    output += _out;
                }
                return output;
            }

            static void Main(string[] args)
        {
            ExpressionTree etree = new ExpressionTree();
            string input;
            Console.WriteLine("----------------------------");
            Implementation imp = new Implementation();
            input = "1+2*3+2+3";
            string postfix = imp.Infix_To_Postfix(ref input);
            Console.WriteLine("infix: " + input);
            Console.WriteLine("Postfix: " + postfix);
            Console.WriteLine("----------------------------");
            etree.BuildTree(postfix);
            etree.Display();
            Console.Write("Prefix : ");
            etree.Prefix();
            Console.Write("Postfix : ");
            etree.Postfix();
            Console.Write("Infix : ");
            etree.ParenthesizedInfix();
            Console.WriteLine("Value : " + etree.Evaluate());
            Console.ReadLine();
        }
    }
}