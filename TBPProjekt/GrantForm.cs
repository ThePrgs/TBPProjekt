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
    public partial class GrantForm : Form
    {
        private NpgsqlConnection con;

        public GrantForm(NpgsqlConnection conn)
        {
            InitializeComponent();
            con = conn;
        }

        private void GrantForm_Load(object sender, EventArgs e)
        {
            con.Open();
            var da = new NpgsqlDataAdapter("SELECT rolname FROM pg_authid;", con);
            var ds = new DataSet();
            var ds2 = new DataSet();
            da.Fill(ds, "Roles");
            da.Fill(ds, "Roles2");
            comboBox1.DisplayMember = "rolname";
            comboBox2.DisplayMember = "rolname";
            comboBox1.ValueMember = "rolname";
            comboBox2.ValueMember = "rolname";
            comboBox1.DataSource = ds.Tables["Roles"];
            comboBox2.DataSource = ds.Tables["Roles2"];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form newForm = new DbForm(con);
            newForm.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            var query = new NpgsqlCommand("GRANT " + comboBox1.Text + " TO " + comboBox2.Text + ";", con);
            query.ExecuteNonQuery();
            con.Close();
            this.Hide();
            Form newForm = new DbForm(con);
            newForm.ShowDialog();
            this.Close();
        }
    }
}
