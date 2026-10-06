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

        List<string> ertekek = new List<string>();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            meretLista.SelectedIndex = 1;
            temaLista.SelectedIndex = 0;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
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

            ertekek = AdatokLetrehozasa();
            ertekek = ertekek.OrderBy(x => random.Next()).ToList();
            int index = 0;

            for (int i = 0; i < meret; i++)
            {
                for (int j = 0; j < meret; j++)
                {
                    Button gomb = new Button();
                    gomb.Content = "?";
                    gomb.FontSize = 24;
                    gomb.Tag = ertekek[index];
                    gomb.Margin = new Thickness(3);

                    gomb.Click += Gomb_Click;

                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb, j);

                    sorok.Children.Add(gomb);
                    index++;
                }
            }
            jatekTer.Children.Add(sorok);
        }

        private List<string> AdatokLetrehozasa()
        {
            List<string> lista = new List<string>();

            if (temaLista.SelectedIndex == 0)
            {
                for (int i = 1; i <= (meret * meret) / 2; i++)
                {
                    lista.Add(i.ToString());
                    lista.Add(i.ToString());
                }
            }
            else if (temaLista.SelectedIndex == 1)
            {
                string[] smiley =
                {
                    ":)", ":(", ":D", ";)", ":P", "XD",
                    "B)", ":O", ":|", ":'(", ":/", ":3",
                    "^_^", "^^", "-_-", "UwU", "o_O", ">:("
                };

                int parokSzama = (meret * meret) / 2;
                for (int i = 0; i < parokSzama; i++)
                {
                    lista.Add(smiley[i]);
                    lista.Add(smiley[i]);
                }
            }

            else if (temaLista.SelectedIndex == 2)
            {
                string[] orszagok =
                {
                    "Magyarország", "Budapest", "Franciaország", "Párizs", "Németország", "Berlin", "Olaszország", "Róma", "Spanyolország", "Madrid", "Ausztria", "Bécs", "Japán", "Tokió", "Kanada", "Ottawa", "Ausztrália", "Canberra"
                };
                int parokSzama = (meret * meret) / 2;

                for (int i = 0; i < parokSzama; i++)
                {
                    lista.Add(orszagok[i]);
                    lista.Add(orszagok[i]);
                }
            }
            return lista;
        }

        private void Gomb_Click(object sender, RoutedEventArgs e)
        {
            if (!kattinthato)
            {
                return;
            }

            Button gomb = (Button)sender;

            if (gomb == elsoGomb)
            {
                return;
            }

            gomb.Content = gomb.Tag;

            if (elsoGomb == null)
            {
                elsoGomb = gomb;
            }
            else
            {
                masodikGomb = gomb;

                probalkozasok++;
                probalkozasText.Text = "Próbálkozások: " + probalkozasok;

                kattinthato = false;

                if (elsoGomb.Tag.ToString() == masodikGomb.Tag.ToString())
                {
                    elsoGomb.IsEnabled = false;
                    masodikGomb.IsEnabled = false;

                    megtalaltParok++;

                    elsoGomb = null;
                    masodikGomb = null;

                    kattinthato = true;

                    if (megtalaltParok == (meret * meret) / 2)
                    {
                        allapotText.Text =
                            "Gratulálok! " + probalkozasok +
                            " próbálkozásból megtaláltad az összes párt!";
                    }
                }
                else
                {
                    timer.Start();
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            elsoGomb.Content = "?";
            masodikGomb.Content = "?";

            elsoGomb = null;
            masodikGomb = null;

            kattinthato = true;
        }
    }
}
