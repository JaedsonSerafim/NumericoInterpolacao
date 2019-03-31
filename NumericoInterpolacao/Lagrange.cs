using System;

namespace NumericoInterpolacao
{
    public struct Lagrange
    {
        readonly int Length;
        readonly double[] Xs;
        readonly double[] Ys;

        public Lagrange(double[] xs, double[] ys)
        {
            if (xs.Length != ys.Length)
                throw new ArgumentException("Vetores de comprimento diferente.");

            Xs = xs;
            Ys = ys;
            Length = xs.Length;
        }

        public Polinomio CalcularPolinomio()
        {
            Polinomio somatorio = default(Polinomio);
            for (int i = 0; i < Length; i++)
            {
                var l = CalcularL(i);
                var fxL = l * Ys[i];
                somatorio = somatorio.Vazio ? fxL : somatorio + fxL;
            }
            return somatorio;
        }

        Polinomio CalcularL(int index)
        {
            Polinomio numerador = default(Polinomio);
            double denominador = 1;
            for (int i = 0; i < Length; i++)
            {
                if (i == index) continue;
                var polinomio = new Polinomio(new Incognita[2] { Incognita.X, (Incognita)(-Xs[i]) });
                numerador = numerador.Vazio ? polinomio : numerador * polinomio;
                denominador *= Xs[index] - Xs[i];
            }
            return numerador * (1 / denominador);
        }
    }
}
