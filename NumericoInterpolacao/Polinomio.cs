using System;
using System.Linq;

namespace NumericoInterpolacao
{
    public struct Polinomio
    {
        Incognita[] Incognitas;

        public Polinomio(Incognita[] incognitas)
        {
            Incognitas = incognitas;
        }

        public double Calcular(double x) => Incognitas.Sum(y => y.Coeficiente * Math.Pow(x, y.Grau));

        public Polinomio Primitiva => new Polinomio((from x in Incognitas
                                                     select new Incognita(x.Grau + 1, x.Coeficiente / x.Grau)).ToArray());
        public Polinomio Derivada => new Polinomio((from x in Incognitas
                                                    where x.Grau != 0
                                                    select new Incognita(x.Grau - 1, x.Coeficiente * x.Grau)).ToArray());

        public bool Vazio => (Incognitas?.Length ?? 0) == 0;
        int MaxGrau => (Incognitas?.Length ?? 0) == 0 ? 0 : Incognitas.Max(x => x.Grau);
        int MinGrau => (Incognitas?.Length ?? 0) == 0 ? 0 : Incognitas.Min(x => x.Grau);
        Incognita GetIncognita(int grau) => ((Incognitas?.Length ?? 0) == 0 ? false : Incognitas.Any(x => x.Grau == grau))
            ? Incognitas.First(x => x.Grau == grau) : new Incognita(grau, 0);

        public static Polinomio operator +(Polinomio var1, Polinomio var2)
        {
            var maxGrau = var1.MaxGrau > var2.MaxGrau ? var1.MaxGrau : var2.MaxGrau;
            var minGrau = var1.MinGrau < var2.MinGrau ? var1.MinGrau : var2.MinGrau;
            var newIncognitas = new Incognita[maxGrau - minGrau + 1];
            for (int i = minGrau, j = 0; i <= maxGrau; i++)
            {
                Incognita inc1 = var1.GetIncognita(i), inc2 = var2.GetIncognita(i);
                newIncognitas[j++] = inc1 + inc2;
            }
            return new Polinomio(newIncognitas.Where(x => x.Coeficiente != 0).ToArray());
        }

        public static Polinomio operator *(Polinomio var1, Polinomio var2)
        {
            Polinomio resultante = default(Polinomio);
            for (int i = 0; i < var2.Incognitas.Length; i++)
            {
                var x = var2.Incognitas[i];
                resultante += new Polinomio((from inc in var1.Incognitas
                                             let transf = x * inc
                                             where transf.Coeficiente != 0
                                             select transf).ToArray());
            }
            return resultante;
        }

        public static Polinomio operator *(Polinomio var1, double var2)
            => new Polinomio(var1.Incognitas.Select(x => x * var2).ToArray());

        public override string ToString()
        {
            string retorno = string.Empty;
            bool primeiro = true;
            foreach (var item in Incognitas.OrderBy(x => x.Grau))
            {
                if (!primeiro)
                    retorno += item.Coeficiente >= 0 ? " + " : " - ";
                retorno += $"{item.Coeficiente.ToString("e2").Replace("-", string.Empty)}*x^{item.Grau}";
                primeiro = false;
            }
            return retorno;
        }
    }
}
