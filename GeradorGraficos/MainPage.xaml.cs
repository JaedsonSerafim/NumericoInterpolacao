using GeradorGraficos.Interpolacao;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Uwp;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace GeradorGraficos
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string Text
        {
            get
            {
                txtValores.Document.GetText(TextGetOptions.AdjustCrlf, out string text);
                return text;
            }
        }
        int MetodoEscolhido { get; set; } = 0;

        const string ApenasNumerico = "0123456789,-";
        const string CaracteresPermitidos = "0123456789,\r\n -";

        SeriesCollection Pontos = new SeriesCollection();
        IFuncao Calculado;

        public MainPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanging(RichEditBox sender, RichEditBoxTextChangingEventArgs args)
        {
            sender.Document.GetText(TextGetOptions.AdjustCrlf, out string text);
            if (text.Length - 1 >= 0 && text.Any(x => !CaracteresPermitidos.Contains(x)))
            {
                var letra = text.First(x => !CaracteresPermitidos.Contains(x));
                sender.Document.SetText(TextSetOptions.ApplyRtfDocumentDefaults, text.Replace(letra.ToString(), string.Empty));
                sender.Document.GetText(TextGetOptions.AdjustCrlf, out text);
                sender.Document.Selection.SetRange(text.Length, text.Length);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtCalculado.Text = string.Empty;
            Pontos.Clear();
            if (Validar())
            {
                var pares = GetPares();
                double[] xs = pares.Select(x => double.Parse(x[0])).ToArray(),
                    ys = pares.Select(x => double.Parse(x[1], System.Globalization.NumberStyles.Float)).ToArray();

                var medidos = xs.Zip(ys, (x, y) => new ObservablePoint(x, y)).ToArray();
                Pontos.Add(new ScatterSeries()
                {
                    Title = "Valor medido: ",
                    Values = new ChartValues<ObservablePoint>(medidos)
                });

                if (MetodoEscolhido == 0)
                {
                    var lagrange = new Lagrange(xs, ys);
                    Calculado = lagrange.CalcularPolinomio();
                    txtDesvio.Text = "0";
                }
                else
                {
                    var mmq = new MMQ(xs, ys, MetodoEscolhido == 1);
                    Calculado = mmq.CalcularPolinomio();
                    txtDesvio.Text = xs.Zip(ys, (x, y) => y - Calculado.Calcular(x)).Sum(x => x * x).ToString();
                }
                txtFuncao.Text = Calculado.ToString();

                double max = xs.Max(), min = xs.Min(), passo = (max - min) / (2 << 4);
                var xsGrafico = new double[(2 << 4) + 1];
                for (int j = 0; j <= 2 << 4; j++)
                    xsGrafico[j] = min + j * passo;
                Pontos.Add(new LineSeries
                {
                    Title = "f(x) = ",
                    LineSmoothness = 1,
                    Values = new ChartValues<ObservablePoint>(xsGrafico.Select(x => new ObservablePoint(x, Calculado.Calcular(x))))
                });
            }
        }

        bool Validar() => GetPares().All(x => x.Length == 2 && x[0].Length > 0 && x[1].Length > 0);
        IEnumerable<string[]> GetPares() => Text.Split('\r', '\n').Select(x => x.Split(' '));

        private void TxtValorX_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            var texto = sender.Text;
            var index = texto.Length - 1;
            if (index >= 0 && texto.Any(x => !ApenasNumerico.Contains(x)))
            {
                var letra = texto.First(x => !ApenasNumerico.Contains(x));
                sender.Text = texto.Replace(letra.ToString(), string.Empty);
                texto = sender.Text;
                index = texto.Length;
                sender.Select(index, 0);
            }

            if (((Calculado is Polinomio polinomio && !polinomio.Vazio) || Calculado != null) && double.TryParse(texto, out double numero))
                txtCalculado.Text = Calculado.Calcular(numero).ToString();
        }
    }
}
