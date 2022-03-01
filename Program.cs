using System;

namespace MO_Lab1
{
    public delegate double Function(double x);
    class Program
    {
        static void Main(string[] args)
        {

            Function func = (double x) => { return (x - 1) * (x - 1); };

            var test = new FibonacciMethod(func, -2, 20);
            var temp = test.StartSolver();

        }
    }
}
