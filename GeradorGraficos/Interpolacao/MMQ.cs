using System;
using System.Linq;

namespace GeradorGraficos.Interpolacao
{
    public sealed class MMQ : MetodoInterpolacao
    {
        readonly bool Reta;

        public MMQ(double[] xs, double[] ys, bool reta) : base(xs, ys)
        {
            Reta = reta;
        }

        public override IFuncao CalcularPolinomio()
        {
            var ys = Reta ? Ys : Ys.Select(y => Math.Log(y));
            double sX = Xs.Sum(),
                sX2 = Xs.Sum(x => x * x),
                sXY = Xs.Zip(ys, (x, y) => x * y).Sum(),
                sY = ys.Sum();
            double a = ((Length * sXY) - (sX * sY)) / ((Length * sX2) - (sX * sX)),
                b = (sY - (a * sX)) / Length;
            return Reta ? (IFuncao)new FuncaoReta(a, b) : new FuncaoExponencial(Math.Exp(b), a);
        }
    }
}
