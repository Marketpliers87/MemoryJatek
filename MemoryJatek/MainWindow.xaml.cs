using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemoryJatek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();

        int meret = 0;
        int probalkozasok = 0;
        int megtalaltParok = 0;

        Button elsoGomb = null;
        Button masodikGomb = null;

        bool kattinthato = true;

        List<string> ertelel = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            meretLista.SelectedIndex = 1;
            temaLista.SelectedIndex = 0;
        }

        private void meretLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (meretLista.SelectedIndex == 0)
            {
                meret = 2;
            }
            else if (meretLista.SelectedIndex == 1)
            {
                meret = 4;
            }
            else if (meretLista.SelectedIndex == 2)
            {
                meret = 6;
            }
        }
        private void inditasGomb_Click(object sender, RoutedEventArgs e)
        {
            if (meret == 0)
            {
                MessageBox.Show("Válassz játéktábla méretet!");
                return;
            }
            if (temaLista.SelectedIndex == -1)
            {
                MessageBox.Show("Válassz pályát!");
                return;
            }
            UjJatek();
        }
        private void UjJatek()
        {
            jatekTer.Children.Clear();

            elsoGomb = null;
            masodikGomb = null;

            probalkozasok = 0;
            megtalaltParok = 0;
            kattinthato = true;

            probalkozasText.Text = "Próbálkozások: 0";
            allapotText.Text = "Találd meg a párokat";
            Grid sorok = new Grid();

            for (int i = 0; i < meret; i++)
            {
                sorok.RowDefinitions.Add(new RowDefinition());
                sorok.ColumnDefinitions.Add(new ColumnDefinition());
            }

        }
    }
}
