using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBPProjekt
{
    public partial class Form1 : Form
    {
        public NpgsqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
                string conString = "Host=localhost:5432;Username=" + usernameTxtbox.Text + ";Password=" + passwordTxtbox.Text + ";Database=postgres";
                conn = new NpgsqlConnection(conString);
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    this.Hide();
                    Form newForm = new DbForm(conn);
                    newForm.ShowDialog();
                    this.Close();
                }
        }
    }
}
