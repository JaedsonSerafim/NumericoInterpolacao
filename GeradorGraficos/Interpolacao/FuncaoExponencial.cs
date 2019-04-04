using System;

namespace GeradorGraficos.Interpolacao
{
    public sealed class FuncaoExponencial : IFuncao
    {
        public readonly double Coeficiente;
        public readonly double CoeficienteX;

        public FuncaoExponencial(double coeficiente, double coeficienteX)
        {
            Coeficiente = coeficiente;
            CoeficienteX = coeficienteX;
        }

        public double Calcular(double x) => Coeficiente * Math.Exp(CoeficienteX * x);
        public override string ToString() => $"{Coeficiente}*e^({CoeficienteX}*x)";
    }
}
