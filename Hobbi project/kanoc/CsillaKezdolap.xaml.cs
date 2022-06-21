using System.Windows;

namespace kanoc
{
    /// <summary>
    /// Interaction logic for CsillaKezdolap.xaml
    /// </summary>
    public partial class CsillaKezdolap : Window
    {
        public CsillaKezdolap()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Csilla secondWindow = new Csilla();
            this.Close();
            secondWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            KimenoSzamlak secondWindow = new KimenoSzamlak();
            this.Close();
            secondWindow.Show();
        }
    }
}
