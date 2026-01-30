using System;
using System.Collections.Generic;
using System.Text;

namespace L09_Calculator
{
    public class Calculator
    {
        public int Add(int a, int b) 
        { 
            return a + b; 
        }

        public int Subtract(int a, int b) => a - b;

        public int Multiply(int a, int b) => a * b;

        public double Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return (double)a / b;
        }
    }
}
