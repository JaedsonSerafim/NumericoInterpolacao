using System;
using System.Globalization;
using System.Linq;

namespace NumericoInterpolacao
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] xs, ys;
            if (args.Length < 2)
                throw new ArgumentNullException("Faltam pontos para serem calculados.");
            else if (args.Length % 2 != 0)
                throw new ArgumentNullException("Quantidade de pontos fora do esperado.");
            else
            {
                var nums = args.Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToArray();
                int quant = nums.Length / 2, skip = (nums.Length - 1) / 2;
                xs = new double[quant];
                ys = new double[quant];
                for (int i = 0; i < quant; i++)
                {
                    xs[i] = nums[i];
                    ys[i] = nums[i + quant];
                }
            }

            Console.WriteLine("Pontos informados:");
            Console.Write("X: \t");
            for (int i = 0; i < xs.Length; i++)
                Console.Write($"{xs[i].ToString("e2")}\t");

            Console.Write("\nY: \t");
            for (int i = 0; i < ys.Length; i++)
                Console.Write($"{ys[i].ToString("e2")}\t");

            var lagrange = new Lagrange(xs, ys);
            var polinomio = lagrange.CalcularPolinomio();
            Console.Write("\n\nFuncao encontrada:\nf(x)= ");
            Console.WriteLine(polinomio);

            Console.WriteLine("\n\nCalcule valores de f(x), bastando apenas digitar o x.\nPara sair, apenas pressione Enter.");
            while (true)
            {
                Console.Write("x=");
                string digitado = Console.ReadLine();
                if (string.IsNullOrEmpty(digitado)
                    || !double.TryParse(digitado, NumberStyles.Float, CultureInfo.InvariantCulture, out double numero)) break;
                Console.Write("f(x)=");
                Console.WriteLine(polinomio.Calcular(numero).ToString("e4"));
            }
        }
    }
}
