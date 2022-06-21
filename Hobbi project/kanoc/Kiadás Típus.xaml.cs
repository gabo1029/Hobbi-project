using MySql.Data.MySqlClient;
using System.Windows;
using System.Runtime.InteropServices;

namespace kanoc
{
    /// <summary>
    /// Interaction logic for Kiadás_Típus.xaml
    /// </summary>
    public partial class Kiadás_Típus : Window
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);
        public static string myInt = "ASD";
        
        public Kiadás_Típus()
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                MySqlConnection conn = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember");
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("Ellenőrizd az internetkapcsolatot!","Figyelmeztetés!!");
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                using (MySqlConnection conn = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember"))
                {

                    try
                    {
                        using (var cmd = new MySqlCommand("INSERT INTO `KiadasTipusok` (`id`, `kiadasTipus`) VALUES (NULL, @KiadasTipus);", conn))
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("id", 0);
                            cmd.Parameters.AddWithValue("kiadasTipus", KiadasTipusHozzaadas.Text);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show("Sikeres Hozzáadás!");
                            myInt = "UPDATE";
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        conn.Close();

                    }
                }
            }
            else
            {
                MessageBox.Show("Ellenőrizd az internetkapcsolatot!", "Figyelmeztetés!!");
            }
        }



    }
}
