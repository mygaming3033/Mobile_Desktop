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
    public partial class Zaduzeno : Form
    {
        public Zaduzeno()
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM zahtjevi WHERE Status='Zadužen';", con))
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
        private void Zaduzeno_Load(object sender, EventArgs e)
        {
            disp_data();
        }
        Thread thread;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            INIFile inif = new INIFile(startupPath + "/postavke.ini");
            string server = inif.Read("Database", "server");
            string user = inif.Read("Database", "user id");
            string pass = inif.Read("Database", "password");
            string db = inif.Read("Database", "databasename");
            string ime = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string prezime = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string conString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=" + db + ";";
            MySqlConnection connect = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT id FROM korisnici WHERE Ime='" + ime + "' AND Prezime='"+prezime+"';");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            connect.Open();
            int id = int.Parse(cmd.ExecuteScalar().ToString());
            TeninaGarderoba.idKorisnik = id.ToString();
            thread = new Thread(openKor);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void openKor(object obj)
        {
            Application.Run(new Korisnik());
        }
    }
}
