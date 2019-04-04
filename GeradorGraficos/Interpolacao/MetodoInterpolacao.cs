using System;

namespace GeradorGraficos.Interpolacao
{
    public abstract class MetodoInterpolacao
    {
        protected readonly int Length;
        protected readonly double[] Xs;
        protected readonly double[] Ys;

        public MetodoInterpolacao(double[] xs, double[] ys)
        {
            if (xs.Length != ys.Length)
                throw new ArgumentException("Vetores de comprimento diferente.");
            Xs = xs;
            Ys = ys;
            Length = xs.Length;
        }

        public abstract IFuncao CalcularPolinomio();
    }


}
