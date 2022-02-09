//using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TeninaGarderoba
{
    public partial class TeninaGarderoba : Form
    {
        public TeninaGarderoba()
        {
            InitializeComponent();
        }
        public void disp_data()
        {
            try
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
                    using (MySqlCommand cmd = new MySqlCommand("SELECT id,Ime,Prezime,Nosnja,Obuca,BrObuce,Datum_Zaduzen,Status FROM zahtjevi WHERE Status NOT IN ('Razdužen')", con))
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        public static string idKorisnik = "";
        private void Main_Load(object sender, EventArgs e)
        {
            disp_data();
        }
        Thread thread;
        private void prikažiKorisnikeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thread = new Thread(openKorisnici);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void openKorisnici(object obj)
        {
            Application.Run(new Korisnici());
        }
        private void openKorPodaci(object obj)
        {
            Application.Run(new KoriPodaci());
        }
        private void openKor(object obj)
        {
            Application.Run(new Korisnik());
        }
        private void openZad(object obj)
        {
            Application.Run(new Zaduzeno());
        }
        private void openGard(object obj)
        {
            Application.Run(new Garderoba());
        }
        private void openZahtjevi(object obj)
        {
            Application.Run(new Zahtjevi());
        }
        private void korisničkiPodaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thread = new Thread(openKorPodaci);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void buttonserachkor_Click(object sender, EventArgs e)
        {
            string iid = textBoxIDkor.Text;
            idKorisnik = iid;
            try
            {
                if (iid == "")
                {
                    throw new Exception("Unesi korisnički broj (ID)");
                }
                string startupPath = System.IO.Directory.GetCurrentDirectory();
            INIFile inif = new INIFile(startupPath + "/postavke.ini");
            string server = inif.Read("Database", "server");
            string user = inif.Read("Database", "user id");
            string pass = inif.Read("Database", "password");
            string db = inif.Read("Database", "databasename");
        
            string conString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=" + db + ";";
            MySqlConnection connect = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(id) FROM korisnici WHERE id='" + Convert.ToInt32(iid) + "';");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            connect.Open();
            int br = int.Parse(cmd.ExecuteScalar().ToString());
        
                if (br == 0)
                {
                    throw new Exception("Korisnički broj (ID) ne postoji u bazi podataka");
                }

                thread = new Thread(openKor);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void posuđenoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thread = new Thread(openZad);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            disp_data();
        }

        private void prikažiGarderobuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thread = new Thread(openGard);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void prikažiSveZahtjeveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thread = new Thread(openZahtjevi);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            INIFile inif = new INIFile(startupPath + "/postavke.ini");
            string server = inif.Read("Database", "server");
            string user = inif.Read("Database", "user id");
            string pass = inif.Read("Database", "password");
            string db = inif.Read("Database", "databasename");
            string ime = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string prezime= this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string conString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=" + db + ";";
            MySqlConnection connect = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT id FROM korisnici WHERE Ime='" + ime + "' AND Prezime='"+prezime+"';");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            connect.Open();
            int br = int.Parse(cmd.ExecuteScalar().ToString());
            TeninaGarderoba.idKorisnik = br.ToString();
            thread = new Thread(openKor);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
    
}
