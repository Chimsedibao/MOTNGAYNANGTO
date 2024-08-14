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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double s1 = double.Parse(textBox1.Text.Trim());
                double s2 = double.Parse(textBox2.Text.Trim());
                double sum = s1 + s2;
                textBox3.Text = Math.Round(sum, 2).ToString();
                MessageBox.Show(textBox3.Text);
            }
            catch (Exception )
            {
                MessageBox.Show("gà"); 
            }
        }
    }
}
