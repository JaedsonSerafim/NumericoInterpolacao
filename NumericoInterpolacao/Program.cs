using System;
using System.Linq;

namespace NumericoInterpolacao
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] xs = new double[] { 0, 5e-3, 12e-3, 18e-3 },
                ys = new double[] { 1, 6.73e-3, 6.14e-6, 1.52e-8 };

            var lagrange = new Lagrange(xs, ys);
            var polinomio = lagrange.CalcularPolinomio();
            Console.Write("f(x): ");
            Console.WriteLine(polinomio);
            Console.ReadKey();
        }

        static double Integral(Polinomio polinomio, double min, double max)
        {
            var primitiva = polinomio.Primitiva;
            return primitiva.Calcular(max) - primitiva.Calcular(min);
        }
    }
}
