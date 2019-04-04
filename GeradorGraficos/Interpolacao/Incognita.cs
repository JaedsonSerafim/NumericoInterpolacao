namespace GeradorGraficos.Interpolacao
{
    public struct Incognita
    {
        public readonly int Grau;
        public readonly double Coeficiente;
        public static readonly Incognita X = new Incognita(1, 1);

        public Incognita(int grau, double coeficiente)
        {
            Grau = grau;
            Coeficiente = coeficiente;
        }

        public static Incognita operator *(Incognita var1, Incognita var2) => new Incognita(var1.Grau + var2.Grau, var1.Coeficiente * var2.Coeficiente);
        public static Incognita operator /(Incognita var1, Incognita var2) => new Incognita(var1.Grau - var2.Grau, var1.Coeficiente / var2.Coeficiente);

        public static Incognita operator +(Incognita var1, Incognita var2)
        {
            Validar(var1, var2);
            return new Incognita(var1.Grau, var1.Coeficiente + var2.Coeficiente);
        }

        public static Incognita operator -(Incognita var1, Incognita var2)
        {
            Validar(var1, var2);
            return new Incognita(var1.Grau, var1.Coeficiente - var2.Coeficiente);
        }

        public static Incognita operator *(Incognita var1, double var2)
        {
            return new Incognita(var1.Grau, var1.Coeficiente * var2);
        }

        static void Validar(Incognita var1, Incognita var2)
        {
            if (var1.Grau != var2.Grau)
                throw new System.InvalidOperationException("Variáveis de graus diferentes não podem ser multiplicadas.");
        }

        public static explicit operator Incognita(double valor) => new Incognita(0, valor);
    }
}
