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
    public partial class Zahtjevi : Form
    {
        public Zahtjevi()
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
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM zahtjevi", con))
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
            private void Zahtjevi_Load(object sender, EventArgs e)
        {
            disp_data();
            textBoxdz.Text = "";
            textBoxdr.Text = "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBoxdz.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBoxdr.Text = dateTimePicker2.Value.ToShortDateString();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string s1 = string.Empty;
            string s2 = string.Empty;
            string s3 = string.Empty;
            try
            {
                if (checkBoxRazduzen.Checked)
                {
                    s1 = "Razdužen";
                }
                if (checkBoxZaduzen.Checked)
                {
                    s2 = "Zadužen";
                }
                if (checkBoxZaprimljen.Checked)
                {
                    s3 = "Zaprimljen";
                }
                string startupPath = System.IO.Directory.GetCurrentDirectory();
                INIFile inif = new INIFile(startupPath + "/postavke.ini");
                string server = inif.Read("Database", "server");
                string user = inif.Read("Database", "user id");
                string password = inif.Read("Database", "password");
                string db = inif.Read("Database", "databasename");
                string conString = "server=" + server + ";user id=" + user + ";password=" + password + ";database=" + db + ";";
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    if(s1==string.Empty && s2==string.Empty && s3 == string.Empty)
                    {
                        if (textBoxdr.Text == "" && textBoxdz.Text == "")
                        {
                            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM zahtjevi WHERE Ime LIKE '%" + textBoxIme.Text + "%' AND Prezime LIKE '%" + textBoxPrezime.Text + "%'", con))

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
                        else
                        {

                            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM zahtjevi WHERE Ime LIKE '%" + textBoxIme.Text + "%' AND Prezime LIKE '%" + textBoxPrezime.Text + "%' AND DATE_FORMAT(Datum_Zaduzen , '%e.%c.%Y.') LIKE '%" + textBoxdz.Text + "%' AND DATE_FORMAT(Datum_Razduzen , '%e.%c.%Y.') LIKE '%" + textBoxdr.Text + "%'", con))

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
                        if (textBoxdr.Text == "" && textBoxdz.Text == "")
                        {
                            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM zahtjevi WHERE Ime LIKE '%" + textBoxIme.Text + "%' AND Prezime LIKE '%" + textBoxPrezime.Text + "%'AND (Status LIKE '" + s1 + "' OR Status LIKE '" + s2 + "' OR Status LIKE '" + s3 + "')", con))

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
                        else
                        {

                            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM zahtjevi WHERE Ime LIKE '%" + textBoxIme.Text + "%' AND Prezime LIKE '%" + textBoxPrezime.Text + "%' AND DATE_FORMAT(Datum_Zaduzen , '%e.%c.%Y.') LIKE '%" + textBoxdz.Text + "%' AND DATE_FORMAT(Datum_Razduzen , '%e.%c.%Y.') LIKE '%" + textBoxdr.Text + "%' AND (Status LIKE '" + s1 + "' OR Status LIKE '" + s2 + "' OR Status LIKE '" + s3 + "')", con))

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            disp_data();
        }
    }
}
