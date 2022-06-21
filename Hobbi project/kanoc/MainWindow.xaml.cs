using System.Windows;

namespace kanoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Richard secondWindow = new Richard();
            secondWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CsillaKezdolap secondWindow = new CsillaKezdolap();
            secondWindow.Show();
        }
    }
}
