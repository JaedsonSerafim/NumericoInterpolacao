namespace GeradorGraficos.Interpolacao
{
    public sealed class Lagrange : MetodoInterpolacao
    {
        public Lagrange(double[] xs, double[] ys) : base(xs, ys) { }

        public override IFuncao CalcularPolinomio()
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
