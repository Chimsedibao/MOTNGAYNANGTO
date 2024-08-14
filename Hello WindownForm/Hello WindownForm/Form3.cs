using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hello_WindownForm
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
           
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            textBox1.Text = $"Ngày: {dateTimePicker1.Value.Day}, tháng: {dateTimePicker1.Value.Month}, năm: {dateTimePicker1.Value.Year}";
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            DateTime chonngay = new DateTime(2024 , 07 ,01);
            dateTimePicker1.Value = chonngay;
        }
    }
}
