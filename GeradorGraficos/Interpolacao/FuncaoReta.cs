namespace GeradorGraficos.Interpolacao
{
    public sealed class FuncaoReta : IFuncao
    {
        public readonly double A;
        public readonly double B;

        public FuncaoReta(double a, double b)
        {
            A = a;
            B = b;
        }

        public double Calcular(double x) => (A * x) + B;
        public override string ToString() => $"{A}*x + {B}";
    }
}
