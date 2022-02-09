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
    public partial class KoriPodaci : Form
    {
        public KoriPodaci()
        {
            InitializeComponent();
            this.AcceptButton = buttonUpdate;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string username = textBoxUser.Text;
            string password = textBoxPass.Text;
            string password2 = textBoxPass2.Text;
            try
            {
                if (username == "" || password == "" || password2 == "")
                {
                    throw new Exception("Sva polja moraju biti unesena");
                }
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                INIFile inif = new INIFile(startupPath + "/postavke.ini");
                string server = inif.Read("Database", "server");
                string user = inif.Read("Database", "user id");
                string pass = inif.Read("Database", "password");
                string db = inif.Read("Database", "databasename");
                string conString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=" + db + ";";
                MySqlConnection connect = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(id) FROM korisnici WHERE Username='" + username+"';");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connect;
                connect.Open();
                int br = int.Parse(cmd.ExecuteScalar().ToString());
                if (br == 0)
                {
                    throw new Exception("Korisničko ime ne postoji u bazi podataka");
                }
                if (password != password2)
                {
                    throw new Exception("Unesene lozinke se ne podudaraju");
                }
                MySqlCommand cmd1 = new MySqlCommand("UPDATE korisnici SET Password='"+password+"' WHERE Username='"+username+"';");
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = connect;
                cmd1.ExecuteNonQuery();
                MessageBox.Show("Lozinka je uspješno ažurirana", "Uspjeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void KoriPodaci_Load(object sender, EventArgs e)
        {
            
        }
    }
}
