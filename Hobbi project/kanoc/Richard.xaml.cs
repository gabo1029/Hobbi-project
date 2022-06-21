using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows;

namespace kanoc
{
    /// <summary>
    /// Interaction logic for Richard.xaml
    /// </summary>
    public partial class Richard : Window
    {
        public Richard()
        {
            InitializeComponent();
            binddatagrid();
        }

        private void binddatagrid()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["asd"].ConnectionString;
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve` AS `Cég Neve`, `BejovoSzamla`.`honapSzama` AS `Honap`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte` AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS `Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`, `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON `BejovoSzamla`.`fizetveID` = `Fizetve`.`id`; ";
            cmd.Connection = con;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable("Szamla");
            da.Fill(dt);
            g1.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["asd"].ConnectionString;
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "insert into BejovoSzamla values @szamlaSorszam";
            cmd.Parameters.AddWithValue("@szamlaSorszam", szamlasorszamaTB.Text);
            cmd.Connection = con;
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("Sikeres hozzáadás");
                binddatagrid();
            }
        }
    }
}
