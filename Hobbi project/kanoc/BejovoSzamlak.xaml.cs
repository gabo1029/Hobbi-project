using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;







namespace kanoc
{
    /// <summary>
    /// Interaction logic for Csilla.xaml
    /// </summary>

    public partial class Csilla : Window
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);

        string idSzam = "";
        string thisInt;

        public Csilla()
        {
            InitializeComponent();
            thisInt = Kiadás_Típus.myInt;
            binddatagrid();
            honap.Items.Add("1");
            honap.Items.Add("2");
            honap.Items.Add("3");
            honap.Items.Add("4");
            honap.Items.Add("5");
            honap.Items.Add("6");
            honap.Items.Add("7");
            honap.Items.Add("8");
            honap.Items.Add("9");
            honap.Items.Add("10");
            honap.Items.Add("11");
            honap.Items.Add("12");
            using (MySqlConnection conn = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember"))
            {
                try
                {
                    CBfizetve.Items.Clear();
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT fizetveTipus from Fizetve";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        CBfizetve.Items.Add(dr["fizetveTipus"].ToString());
                    }
                    CBfizetesiforma.Items.Clear();
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1 = conn.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT fizetesiFormaNeve from FizetesiForma";
                    cmd1.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        CBfizetesiforma.Items.Add(dr1["fizetesiFormaNeve"].ToString());
                    }
                    CBkiadastipus.Items.Clear();
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2 = conn.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "SELECT * from KiadasTipusok";
                    cmd2.ExecuteNonQuery();
                    DataTable dt2 = new DataTable();
                    MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                    da2.Fill(dt2);
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        CBkiadastipus.Items.Add(dr2["kiadasTipus"].ToString());
                    }
                    CBafaMerteke.Items.Clear();
                    MySqlCommand cmd3 = new MySqlCommand();
                    cmd3 = conn.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "SELECT * from afa";
                    cmd3.ExecuteNonQuery();
                    DataTable dt3 = new DataTable();
                    MySqlDataAdapter da3 = new MySqlDataAdapter(cmd3);
                    da3.Fill(dt3);
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        CBafaMerteke.Items.Add(dr3["afaMerteke"].ToString());
                    }
                    conn.Close();
                }
                catch (Exception)
                {
                    InternetError();
                }
            }
        }
        public void InternetError()
        {
            MessageBox.Show("Ellenőrizd az internetkapcsolatot", "Figyelmeztetés!!");
        }
        private MySqlConnection con = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember");
        public void binddatagrid()
        {
            try
            {
                szerekeztesbtn.IsEnabled = false;
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["asd"].ConnectionString;
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
                    " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
                    " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
                    " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
                    "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
                    " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
                    " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
                    " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
                    " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
                    " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
                    " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` ORDER BY `BejovoSzamla`.`id` DESC; ";
                cmd.Connection = con;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("Szamla");
                da.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void save_Click(object sender, RoutedEventArgs e)
        {
            string asd = "";
            string asdd = "";
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                try
                {
                    using (var cmd = new MySqlCommand("INSERT INTO `BejovoSzamla` (`id`, `kiadasTipusId`," +
                   " `szamlaSorszam`, `cegNeve`, `teljesitesDatuma`, `szamlaKelte`, `szamlaLejarata`," +
                   " `fizetveDatum`, `fizetveID`, `fizetesiFormaId`, `megjegyzes`, `bruttoOsszeg`," +
                   " `nettoOsszeg`, `afa`, `munka`) VALUES(@id,@kiadasTipusId,@szamlaSorszam,@cegNeve," +
                   "@teljesitesDatuma,@szamlaKelte," +
                   "@szamlaLejarata,@fizetveDatum,@fizetveID,@fizetesiFormaId,@megjegyzes,@brutto,@netto,@afa,@munka)", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("id", 0);
                        cmd.Parameters.AddWithValue("kiadasTipusId", CBkiadastipus.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("szamlaSorszam", TBszamlaSorszam.Text);
                        cmd.Parameters.AddWithValue("cegNeve", TBcegNeve.Text);
                        cmd.Parameters.AddWithValue("teljesitesDatuma", DateTeljesites.Text);
                        cmd.Parameters.AddWithValue("szamlaKelte", DateKelte.Text);
                        cmd.Parameters.AddWithValue("szamlaLejarata", DateLejarat.Text);
                        cmd.Parameters.AddWithValue("fizetveDatum", DateDatuma.Text);
                        cmd.Parameters.AddWithValue("fizetveID", CBfizetve.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("fizetesiFormaId", CBfizetesiforma.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("megjegyzes", TBmegjegyzes.Text);
                        cmd.Parameters.AddWithValue("brutto", TBbrutto.Text);
                        cmd.Parameters.AddWithValue("netto", TBnetto.Text);
                        cmd.Parameters.AddWithValue("afa", TBafa.Text);
                        cmd.Parameters.AddWithValue("munka", TBMunka.Text);
                        cmd.ExecuteNonQuery();
                        CBkiadastipus.SelectedIndex = -1;
                        TBszamlaSorszam.Clear();
                        TBcegNeve.Clear();
                        DateTeljesites.SelectedDate = null;
                        DateKelte.SelectedDate = null;
                        DateLejarat.SelectedDate = null;
                        DateDatuma.SelectedDate = null;
                        CBfizetve.SelectedIndex = -1;
                        CBfizetesiforma.SelectedIndex = -1;
                        TBmegjegyzes.Clear();
                        TBbrutto.Clear();
                        TBnetto.Clear();
                        TBafa.Clear();
                        TBMunka.Clear();
                        binddatagrid();
                        con.Close();

                    }
                }
                catch (MySqlException)
                {
                    if (CBkiadastipus.SelectedIndex == -1)
                    {
                        MessageBox.Show("Válasszon Kiadás típust!", "Figyelmeztetés!!");
                        asd = "xd";
                    }
                    if (CBfizetve.SelectedIndex == -1 && (asd != "xd"))
                    {
                        MessageBox.Show("Válassza ki a fizetés állapotát!", "Figyelmeztetés!!");
                        asdd = "xd";
                    }
                    if (CBfizetesiforma.SelectedIndex == -1 && asd != "xd" && asdd != "xd")
                    {
                        MessageBox.Show("Válassza ki a Fizetési formát!", "Figyelmeztetés!!");
                    }
                    con.Close();
                }
            }
            else
            {
                InternetError();
            }
        }
        private void CBafaMerteke_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TBbrutto.Text))
            {
                int afaMerteke = 0;
                afaMerteke = CBafaMerteke.SelectedIndex + 1;
                double netto = 0;
                double brutto = 0;
                double afamerteke = 0;
                if (afaMerteke == 1)
                {
                    brutto = Convert.ToDouble(TBbrutto.Text);
                    netto = brutto * 1;
                    afamerteke = brutto - netto;
                    TBnetto.Text = Convert.ToString(Convert.ToInt32(netto));
                    TBafa.Text = Convert.ToString(Convert.ToInt32(afamerteke));
                }
                else if (afaMerteke == 2)
                {
                    brutto = Convert.ToDouble(TBbrutto.Text);
                    netto = brutto * 0.85;
                    afamerteke = brutto - netto;
                    TBnetto.Text = Convert.ToString(Convert.ToInt32(netto));
                    TBafa.Text = Convert.ToString(Convert.ToInt32(afamerteke));
                }
                else
                {
                    brutto = Convert.ToDouble(TBbrutto.Text);
                    netto = brutto * 0.73;
                    afamerteke = brutto - netto;
                    TBnetto.Text = Convert.ToString(Convert.ToInt32(netto));
                    TBafa.Text = Convert.ToString(Convert.ToInt32(afamerteke));
                }
            }
            else return;
        }
        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumeric(e.Text);
        }
        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);
        }
        public void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                id.Content = row_selected[0].ToString();
                idSzam = row_selected[0].ToString();
                CBkiadastipus.SelectedItem = row_selected[1].ToString();
                TBszamlaSorszam.Text = row_selected["Számla Sorszám"].ToString();
                TBcegNeve.Text = row_selected[3].ToString();
#pragma warning disable CS0252
                if (row_selected[4] != "")
                {
                    DateTeljesites.SelectedDate = Convert.ToDateTime(row_selected[4]);
                }
                if (row_selected[5] != "")
                {
                    DateKelte.SelectedDate = Convert.ToDateTime(row_selected[5]);
                }
                if (row_selected[6] != "")
                {
                    DateLejarat.SelectedDate = Convert.ToDateTime(row_selected[6]);
                }
                if (row_selected[7] != "")
                {
                    DateDatuma.SelectedDate = Convert.ToDateTime(row_selected[7]);
                }
#pragma warning disable CS0252
                CBfizetve.SelectedItem = row_selected[8].ToString();
                CBfizetesiforma.SelectedItem = row_selected[9].ToString();
                TBmegjegyzes.Text = row_selected[10].ToString();
                TBbrutto.Text = row_selected[11].ToString();
                TBnetto.Text = row_selected[12].ToString();
                TBafa.Text = row_selected[13].ToString();
                TBMunka.Text = row_selected[14].ToString();
                save.IsEnabled = false;
                szerekeztesbtn.IsEnabled = true;
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TBszamlaSorszam.Text == "" && TBmegjegyzes.Text == "" && TBbrutto.Text == "")
            {
                CsillaKezdolap secondWindow = new CsillaKezdolap();
                this.Close();
                secondWindow.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Be akarod zárni a Bejövő számlák ablakot ?", "Figyelmeztés!!!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        CsillaKezdolap secondWindow = new CsillaKezdolap();
                        this.Close();
                        secondWindow.Show();
                        break;
                    case MessageBoxResult.No:
                        CsillaKezdolap asdWindow = new CsillaKezdolap();
                        asdWindow.Show();
                        break;
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (TBszamlaSorszam.Text == "" && TBmegjegyzes.Text == "" && TBbrutto.Text == "")
            {
                KimenoSzamlak secondWindow = new KimenoSzamlak();
                this.Close();
                secondWindow.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Be akarod zárni a Bejövő számlák ablakot ?", "Figyelmeztés!!!", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        KimenoSzamlak secondWindow = new KimenoSzamlak();
                        this.Close();
                        secondWindow.Show();
                        break;
                    case MessageBoxResult.No:
                        KimenoSzamlak aWindow = new KimenoSzamlak();
                        aWindow.Show();
                        break;
                }
            }
        }
        private void kiuritesbtn_Click(object sender, RoutedEventArgs e)
        {
            CBkiadastipus.SelectedIndex = -1;
            TBszamlaSorszam.Clear();
            TBcegNeve.Clear();
            DateTeljesites.SelectedDate = null;
            DateKelte.SelectedDate = null;
            DateLejarat.SelectedDate = null;
            DateDatuma.SelectedDate = null;
            CBfizetve.SelectedIndex = -1;
            CBfizetesiforma.SelectedIndex = -1;
            TBmegjegyzes.Clear();
            TBbrutto.Clear();
            TBnetto.Clear();
            TBafa.Clear();
            TBMunka.Clear();
            save.IsEnabled = true;
            szerekeztesbtn.IsEnabled = false;
        }

        private void szerekeztesbtn_Click(object sender, RoutedEventArgs e)
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                using (var cmd = new MySqlCommand("UPDATE `BejovoSzamla` SET `kiadasTipusId` = @kiadasTipusId, `szamlaSorszam` = @szamlaSorszam," +
            "`cegNeve` = @cegNeve, `teljesitesDatuma` =@teljesitesDatuma, `szamlaKelte` = @szamlaKelte," +
            "`szamlaLejarata` = @szamlaLejarata, `fizetveDatum` = @fizetveDatum, `fizetveID` = @fizetveID," +
            "`fizetesiFormaId` = @fizetesiFormaId, `megjegyzes` = @megjegyzes, `bruttoOsszeg` =@brutto, `nettoOsszeg` = @netto," +
            "`afa` = @afa, `munka` = @munka WHERE `BejovoSzamla`.`id` = @id", con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("id", id.Content);
                    cmd.Parameters.AddWithValue("kiadasTipusId", CBkiadastipus.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("szamlaSorszam", TBszamlaSorszam.Text);
                    cmd.Parameters.AddWithValue("cegNeve", TBcegNeve.Text);
                    cmd.Parameters.AddWithValue("teljesitesDatuma", DateTeljesites.Text);
                    cmd.Parameters.AddWithValue("szamlaKelte", DateKelte.Text);
                    cmd.Parameters.AddWithValue("szamlaLejarata", DateLejarat.Text);
                    cmd.Parameters.AddWithValue("fizetveDatum", DateDatuma.Text);
                    cmd.Parameters.AddWithValue("fizetveID", CBfizetve.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("fizetesiFormaId", CBfizetesiforma.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("megjegyzes", TBmegjegyzes.Text);
                    cmd.Parameters.AddWithValue("brutto", TBbrutto.Text);
                    cmd.Parameters.AddWithValue("netto", TBnetto.Text);
                    cmd.Parameters.AddWithValue("afa", TBafa.Text);
                    cmd.Parameters.AddWithValue("munka", TBMunka.Text);
                    cmd.ExecuteNonQuery();
                    CBkiadastipus.SelectedIndex = -1;
                    TBszamlaSorszam.Clear();
                    TBcegNeve.Clear();
                    DateTeljesites.SelectedDate = null;
                    DateKelte.SelectedDate = null;
                    DateLejarat.SelectedDate = null;
                    DateDatuma.SelectedDate = null;
                    CBfizetve.SelectedIndex = -1;
                    CBfizetesiforma.SelectedIndex = -1;
                    TBmegjegyzes.Clear();
                    TBbrutto.Clear();
                    TBnetto.Clear();
                    TBafa.Clear();
                    TBMunka.Clear();
                    binddatagrid();
                    con.Close();
                }
            }
            else
            {
                InternetError();
            }
        }
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                MySqlConnection con = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember");
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = new MySqlCommand();
                adapter.SelectCommand.Connection = con;
                string KiadasdTipusFilter = "";
                KiadasdTipusFilter = Convert.ToString(CBkiadastipus_Copy.SelectedIndex + 1);
                if (CBkiadastipus_Copy.SelectedIndex == -1 && SzamlaSorszamaKeres.Text != "")
                {
                    adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
                " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
                " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
                " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
                "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
                " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
                " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
                " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
                " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
                " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
                " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where  BejovoSzamla.szamlaSorszam like'%" + SzamlaSorszamaKeres.Text + "%' ORDER BY `BejovoSzamla`.`id` DESC";
                }
                else if (CBkiadastipus_Copy.SelectedIndex != -1 && SzamlaSorszamaKeres.Text == "")
                {
                    adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
                " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
                " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
                " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
                "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
                " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
                " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
                " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
                " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
                " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
                " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.kiadasTipusId like '" + KiadasdTipusFilter + "'  ORDER BY `BejovoSzamla`.`id` DESC";
                }
                else if (CBkiadastipus_Copy.SelectedIndex != -1 && SzamlaSorszamaKeres.Text != "")
                {
                    adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
                " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
                " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
                " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
                "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
                " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
                " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
                " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
                " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
                " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
                " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.kiadasTipusId like '" + KiadasdTipusFilter + "' AND BejovoSzamla.szamlaSorszam like'%" + SzamlaSorszamaKeres.Text + "%'  ORDER BY `BejovoSzamla`.`id` DESC";
                }
                else
                {
                    adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
                " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
                " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
                " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
                "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
                " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
                " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
                " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
                " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
                " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
                " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` ORDER BY `BejovoSzamla`.`id` DESC";
                }
                switch (honap.SelectedIndex + 1)
                {
                    case 1:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 01.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 2:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 02.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 3:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 03.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 4:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 04.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 5:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 05.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 6:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 06.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 7:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 07.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 8:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 08.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 9:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 09.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 10:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 10.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 11:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 11.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    case 12:
                        adapter.SelectCommand.CommandText = "SELECT  `BejovoSzamla`.`id` AS ID, `KiadasTipusok`.`kiadasTipus` AS" +
             " `Kiadás`, `BejovoSzamla`.`szamlaSorszam` AS `Számla Sorszám`, `BejovoSzamla`.`cegNeve`" +
             " AS `Cég Neve`, `BejovoSzamla`.`teljesitesDatuma` AS `Teljesítés`, `BejovoSzamla`.`szamlaKelte`" +
             " AS `Kelte`, `BejovoSzamla`.`szamlaLejarata` AS `Lejárat`, `BejovoSzamla`.`fizetveDatum` AS " +
             "`Fizetve`, `Fizetve`.`fizetveTipus` AS `Állapota`, `FizetesiForma`.`fizetesiFormaNeve` AS" +
             " `Fizetési forma`, `BejovoSzamla`.`megjegyzes` AS `Megjegyzes`, `BejovoSzamla`.`bruttoOsszeg` AS" +
             " `Bruttó összeg`, `BejovoSzamla`.`nettoOsszeg` AS `Nettó összeg`, `BejovoSzamla`.`afa` AS `Áfa`," +
             " `BejovoSzamla`.`munka` AS `Munka` FROM `BejovoSzamla` INNER JOIN `KiadasTipusok` ON" +
             " `BejovoSzamla`.`kiadasTipusId` = `KiadasTipusok`.`id` INNER JOIN `FizetesiForma` ON" +
             " `BejovoSzamla`.`fizetesiFormaId` = `FizetesiForma`.`id` INNER JOIN `Fizetve` ON" +
             " `BejovoSzamla`.`fizetveID` = `Fizetve`.`id` where BejovoSzamla.teljesitesDatuma like '%2022. 12.%' ORDER BY `BejovoSzamla`.`id` DESC";
                        break;
                    default:
                        break;
                }
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                datagrid.ItemsSource = dt.DefaultView;
            }
            else
            {
                InternetError();
            }
        }
        private void CBkiadastipus_Copy_DropDownOpened(object sender, EventArgs e)
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                using (MySqlConnection conn = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember"))
                {
                    try
                    {
                        CBkiadastipus_Copy.Items.Clear();
                        conn.Open();
                        MySqlCommand cmd1 = new MySqlCommand();
                        cmd1 = conn.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "SELECT * from KiadasTipusok";
                        cmd1.ExecuteNonQuery();
                        DataTable dt1 = new DataTable();
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        da1.Fill(dt1);
                        foreach (DataRow dr in dt1.Rows)
                        {

                            CBkiadastipus_Copy.Items.Add(dr["kiadasTipus"].ToString());

                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        // write exception info to log or anything else
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                InternetError();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Kiadás_Típus secondWindow = new Kiadás_Típus();
            secondWindow.Show();
        }
        private void CBkiadastipus_DropDownOpened(object sender, EventArgs e)
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                using (MySqlConnection conn = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember"))
                {
                    try
                    {
                        CBkiadastipus.Items.Clear();
                        conn.Open();
                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2 = conn.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT * from KiadasTipusok";
                        cmd2.ExecuteNonQuery();
                        DataTable dt2 = new DataTable();
                        MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                        da2.Fill(dt2);
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            CBkiadastipus.Items.Add(dr2["kiadasTipus"].ToString());
                        }
                        conn.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            else
            {
                InternetError();
            }
        }

        public void deleteBtnClick(object sender, RoutedEventArgs e)
        {
            int Out;
            if (InternetGetConnectedState(out Out, 0) == true)
            {
                using (MySqlConnection conn = new MySqlConnection("SERVER=mysql.nethely.hu; DATABASE=kanocbeton;UID=kanocbeton;PASSWORD=1Pokember"))
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2 = conn.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "DELETE FROM `BejovoSzamla` WHERE `BejovoSzamla`.`id` = @id";
                        cmd2.Parameters.AddWithValue("id", idSzam);
                        cmd2.ExecuteNonQuery();
                        binddatagrid();
                        conn.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            else
            {
                InternetError();
            }
        }

        private void RestoreDatagrid_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Today;
            binddatagrid();
            CBkiadastipus_Copy.SelectedIndex = -1;
            SzamlaSorszamaKeres.Clear();
            honap.SelectedIndex = -1;
            DateTeljesites.SelectedDate = date;
            DateKelte.SelectedDate = date;
            DateLejarat.SelectedDate = date;
            DateDatuma.SelectedDate = date;
        }
    }
}
