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

namespace Hello_WindownForm
{
    public partial class Form4 : Form

    {
        SqlConnection con = new SqlConnection(); 
        string strcon = "Data Source=BILLNG\\SQLEXPRESS;Initial Catalog=hocsinh; Integrated Security=true;";
  

        public Form4()
        {
            InitializeComponent();
           
        }

       

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
            string name = textBox2.Text.Trim();
            string age = textBox3.Text.Trim();
            string address = textBox4.Text.Trim();
            string hometown = textBox5.Text.Trim();

            if (id == "" || name == "" || age == "" || address == "" || hometown == "")
            {
                MessageBox.Show("Các trường khác phải khác rỗng!");
                return;
            }
            else
            {
                string[] row = new string[] { id, name, age, address, hometown };
                dataGridView1.Rows.Add(row);
               
            }
        }
    }
}
