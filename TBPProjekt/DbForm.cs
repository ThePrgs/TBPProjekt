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
    public partial class DbForm : Form
    {
        private NpgsqlConnection con;

        public DbForm(NpgsqlConnection conn)
        {
            InitializeComponent();
            con = conn;
            con.Close();
        }

        private void DbForm_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form newForm = new Form1();
            newForm.ShowDialog();
            this.Close();
        }

        private void DataBind()
        {
            try
            {
                con.Open();
                var cmd = new NpgsqlCommand("SELECT * FROM pg_authid WHERE pg_has_role('"+con.UserName+"', oid, 'member'); ", con);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["rolname"].ReadOnly = true;
                dataGridView1.Columns["oid"].ReadOnly = true;
                dataGridView1.Columns["rolpassword"].ReadOnly = true;
                dataGridView1.Columns["rolvaliduntil"].ReadOnly = true;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                    {
                        if (row.Cells[1].Value.Equals(con.UserName) && row.Cells[2].Value.Equals(false))
                        {
                            button1.Hide();
                            button3.Hide();
                            break;
                        }
                    }
                }
                con.Close();
                }
            catch (PostgresException)
            {
                MessageBox.Show("Permission denied");
                con.Close();
            }
}

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            con.Open();
            NpgsqlCommand query;
           
            query = new NpgsqlCommand("UPDATE pg_authid SET " + dataGridView1.Columns[e.ColumnIndex].Name + " ='" + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + "'WHERE oid='" + dataGridView1.Rows[e.RowIndex].Cells["oid"].Value.ToString() + "';", con);
            query.ExecuteNonQuery();

            con.Close();
            DataBind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form newForm = new CreateForm(con);
            newForm.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form newForm = new GrantForm(con);
            newForm.ShowDialog();
            this.Close();
        }
    }
}
