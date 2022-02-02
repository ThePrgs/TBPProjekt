using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBPProjekt
{
    public partial class CreateForm : Form
    {
        private NpgsqlConnection con;
        public CreateForm(NpgsqlConnection conn)
        {
            InitializeComponent();
            con = conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    con.Open();
                    var query = new NpgsqlCommand("CREATE USER " + textBox1.Text + " WITH PASSWORD '" + textBox2.Text + "';", con);
                    query.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Error");
                }
                con.Close();
                this.Hide();
                Form newForm = new DbForm(con);
                newForm.ShowDialog();
                this.Close();
            }
            catch (PostgresException)
            {
                MessageBox.Show("Error");
                this.Hide();
                Form newForm = new DbForm(con);
                newForm.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form newForm = new DbForm(con);
            newForm.ShowDialog();
            this.Close();
        }
    }
}
