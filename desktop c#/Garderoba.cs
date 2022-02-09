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
    public partial class Garderoba : Form
    {
        public Garderoba()
        {
            InitializeComponent();
        }
        public void disp_nosnje()
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
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM nosnje", con))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void disp_obuca()
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
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM obuca", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                dataGridView2.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Garderoba_Load(object sender, EventArgs e)
        {
            disp_nosnje();
            disp_obuca();
        }
    }
}
