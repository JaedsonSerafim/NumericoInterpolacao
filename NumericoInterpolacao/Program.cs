using System;

namespace NumericoInterpolacao
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] xs = new double[] { -1, 0, 2 },
                ys = new double[] { 4, 1, -1 };
            var lagrange = new Lagrange(xs, ys);
            var polinomio = lagrange.CalcularPolinomio();
            Console.WriteLine(polinomio);
            Console.ReadKey();
        }
    }
}
