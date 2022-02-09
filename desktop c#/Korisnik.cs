using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeninaGarderoba
{
    public partial class Korisnik : Form
    {
        public Korisnik()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void disp_data()
        {
            checkBoxAll.Checked = false;
            string korisnikid = TeninaGarderoba.idKorisnik;
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            INIFile inif = new INIFile(startupPath + "/postavke.ini");
            string server = inif.Read("Database", "server");
            string user = inif.Read("Database", "user id");
            string password = inif.Read("Database", "password");
            string db = inif.Read("Database", "databasename");
            string conString = "server=" + server + ";user id=" + user + ";password=" + password + ";database=" + db + ";";
            //================================
            MySqlConnection connect = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM korisnici WHERE id='" + Convert.ToInt32(korisnikid) + "'");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            connect.Open();
            try
            {
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    labelID.Text = dr.GetInt32("id").ToString();
                    labelIme.Text = dr.GetString("Ime");
                    labelPrezime.Text = dr.GetString("Prezime");
                    labelUser.Text = dr.GetString("Username");
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
                //================================
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    using (MySqlCommand cmd1 = new MySqlCommand("SELECT id,Nosnja,Obuca,BrObuce,Datum_Zaduzen,Status FROM zahtjevi WHERE Ime='"+labelIme.Text+"'AND Prezime='"+labelPrezime.Text+"' AND Status NOT IN ('Razdužen');", con))
                    {
                        cmd1.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd1))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                dataGridView1.DataSource = dt;
                            }
                        }
                    }
                }
            }
            labelBroj.Text = dataGridView1.Rows.Count.ToString();
        }
        private void Korisnik_Load(object sender, EventArgs e)
        {
            disp_data();
        }

        private void buttonzaduzi_Click(object sender, EventArgs e)
        {
            try
            {
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                INIFile inif = new INIFile(startupPath + "/postavke.ini");
                string server = inif.Read("Database", "server");
                string user = inif.Read("Database", "user id");
                string pass = inif.Read("Database", "password");
                string db = inif.Read("Database", "databasename");
                string conString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=" + db + ";";
                MySqlConnection connect = new MySqlConnection(conString);
                if (this.dataGridView1.Rows.Count==0)
                {
                    throw new Exception("Nije odabran zahtjev.");
                }
                string id = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
              
                MySqlCommand cmd1 = new MySqlCommand("UPDATE zahtjevi SET Status='Zadužen',Datum_Zaduzen=CURDATE() WHERE id='" + Convert.ToInt32(id) + "';");
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = connect;
                connect.Open();
                cmd1.ExecuteNonQuery();
                disp_data();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void buttonrazduzi_Click(object sender, EventArgs e)
        {
            try
            {
              string startupPath = System.IO.Directory.GetCurrentDirectory();
            INIFile inif = new INIFile(startupPath + "/postavke.ini");
            string server = inif.Read("Database", "server");
            string user = inif.Read("Database", "user id");
            string pass = inif.Read("Database", "password");
            string db = inif.Read("Database", "databasename");
            string conString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=" + db + ";";
            MySqlConnection connect = new MySqlConnection(conString);
            if (this.dataGridView1.Rows.Count == 0)
            {
                throw new Exception("Nije odabran zahtjev.");
            }
            string id = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            MySqlCommand cmd1 = new MySqlCommand("UPDATE zahtjevi SET Status='Razdužen',Datum_Razduzen=CURDATE() WHERE id='" + Convert.ToInt32(id) + "';");
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = connect;
            connect.Open();
            cmd1.ExecuteNonQuery();
            disp_data();
        }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

}

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAll.Checked)
            {
                string korisnikid = TeninaGarderoba.idKorisnik;
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                INIFile inif = new INIFile(startupPath + "/postavke.ini");
                string server = inif.Read("Database", "server");
                string user = inif.Read("Database", "user id");
                string password = inif.Read("Database", "password");
                string db = inif.Read("Database", "databasename");
                string conString = "server=" + server + ";user id=" + user + ";password=" + password + ";database=" + db + ";";
                //================================
                MySqlConnection connect = new MySqlConnection(conString);
                connect.Open();
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    using (MySqlCommand cmd1 = new MySqlCommand("SELECT id,Nosnja,Obuca,BrObuce,Datum_Zaduzen,Status FROM zahtjevi WHERE Ime='" + labelIme.Text + "'AND Prezime='" + labelPrezime.Text + "';", con))
                    {
                        cmd1.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd1))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                dataGridView1.DataSource = dt;
                            }
                        }
                    }
                }
                labelBroj.Text = dataGridView1.Rows.Count.ToString();
            }
            else
            {
                disp_data();
            }
        }
    }
}
