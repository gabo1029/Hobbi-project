using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows;


namespace kanoc
{
    /// <summary>
    /// Interaction logic for KimenoSzamlak.xaml
    /// </summary>
    public partial class KimenoSzamlak : Window
    {
        public KimenoSzamlak()
        {
            InitializeComponent();
            binddatagrid();
        }

        public void binddatagrid()
        {
            using (MySqlConnection con = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember"))
            {

                con.ConnectionString = ConfigurationManager.ConnectionStrings["asd"].ConnectionString;
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT `KimenoSzamalk`.`id`, `KimenoSzamalk`.`munkaSzam` AS `Munka Szám`, " +
                    "`KimenoSzamalk`.`szamlaSorszam` AS `Számla Sorszám`, `KimenoSzamalk`.`cegNeve` AS `Cég neve`," +
                    " `KimenoSzamalk`.`teljesitesDatuma` AS `Teljesítés Dátuma`, `KimenoSzamalk`.`szamlaKelte` AS `Számla Kelte`," +
                    " `KimenoSzamalk`.`szamlaLejarata` AS `Számla Lejárata`, `KimenoSzamalk`.`fizetveDatuma` AS `Fizetés Dátuma`, " +
                    " Fizetve.fizetveTipus AS `Fizetve`, `FizetesiForma`.`fizetesiFormaNeve` AS `Fizetési Forma`, " +
                    "`KimenoSzamalk`.`munkaNev` AS `Munka Neve`, `KimenoSzamalk`.`bruttoOsszeg` AS `Bruttó összeg`, " +
                    "`KimenoSzamalk`.`nettoOsszeg` AS `Nettó Összeg`, `KimenoSzamalk`.`afa` AS `Áfa`, " +
                    " `KimenoSzamalk`.`visszatartasOsszege` AS `Visszatartás összege`, " +
                    "`KimenoSzamalk`.`visszatartasLejarata` AS `Visszatartás Lejárata`, `KimenoSzamalk`.`orak` AS `Órák`, " +
                    "`KimenoSzamalk`.`oradij` AS `Óradíj`, `KimenoSzamalk`.`megjegyzes` AS `Megjegyzés`, `KimenoSzamalk`.`egyeb` AS `Egyéb`" +
                    "FROM `KimenoSzamalk` INNER JOIN `Fizetve` ON `KimenoSzamalk`.`fizetveId` = `Fizetve`.`id` INNER JOIN `FizetesiForma` ON KimenoSzamalk.fizetesiFormaId = `FizetesiForma`.`id`";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("Szamla");
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                con.Close();
                
            }

        }
    }
}
