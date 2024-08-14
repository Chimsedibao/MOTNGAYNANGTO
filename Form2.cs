using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Hello_WindownForm
{
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=BILLNG\\SQLEXPRESS;Initial Catalog=CSDLTinhThanh; Integrated Security=true;");

        public Form2()
        {
            InitializeComponent();
            try
            {
                con.Open();
                SqlCommand sc = new SqlCommand("select (TenTT) from tinhthanh ", con);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("TenTT", typeof(string));
                dt.Load(reader);
                comboBox1.ValueMember = "TenTT";  
                comboBox1.DataSource = dt; 
                con.Close(); 

            }
            catch (Exception ex) { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (string)comboBox1.SelectedValue; 
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
