using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeninaGarderoba
{
    public partial class Korisnici : Form
    {
        public Korisnici()
        {
            InitializeComponent();
        }
        public void disp_data()
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            INIFile inif = new INIFile(startupPath + "/postavke.ini");
            string server = inif.Read("Database", "server");
            string user = inif.Read("Database", "user id");
            string password = inif.Read("Database", "password");
            string db = inif.Read("Database", "databasename");
            string conString = "server=" + server + ";user id=" + user + ";password=" + password + ";database=" + db + ";";
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT id as ID, Username as Korisničko_ime, Ime, Prezime FROM korisnici", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
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
        private void Korisnici_Load(object sender, EventArgs e)
        {
            disp_data();
        }
        Thread thread;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TeninaGarderoba.idKorisnik = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            thread = new Thread(openKor);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void openKor(object obj)
        {
            Application.Run(new Korisnik());
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text==string.Empty) {
                disp_data();
            }
            else
            {
                if (textBox3.Text[0] > 48 && textBox3.Text[0] < 58)
                {
                    string startupPath = System.IO.Directory.GetCurrentDirectory();
                    INIFile inif = new INIFile(startupPath + "/postavke.ini");
                    string server = inif.Read("Database", "server");
                    string user = inif.Read("Database", "user id");
                    string password = inif.Read("Database", "password");
                    string db = inif.Read("Database", "databasename");
                    string conString = "server=" + server + ";user id=" + user + ";password=" + password + ";database=" + db + ";";
                    using (MySqlConnection con = new MySqlConnection(conString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("SELECT id as ID, Username as Korisničko_ime, Ime, Prezime FROM korisnici WHERE id LIKE '%" + textBox3.Text + "%'", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
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
                else
                {
                    string startupPath = System.IO.Directory.GetCurrentDirectory();
                    INIFile inif = new INIFile(startupPath + "/postavke.ini");
                    string server = inif.Read("Database", "server");
                    string user = inif.Read("Database", "user id");
                    string password = inif.Read("Database", "password");
                    string db = inif.Read("Database", "databasename");
                    string conString = "server=" + server + ";user id=" + user + ";password=" + password + ";database=" + db + ";";
                    using (MySqlConnection con = new MySqlConnection(conString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("SELECT id as ID, Username as Korisničko_ime, Ime, Prezime FROM korisnici WHERE CONCAT(Ime,' ',Prezime,' ',Username) LIKE '%" + textBox3.Text + "%'", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
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
            }
          
        }
    }
}
