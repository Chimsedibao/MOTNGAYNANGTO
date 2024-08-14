using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; //thuvien ket noi csdl 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hello_WindownForm
{
    public partial class DuAn : Form
    {      
        private SqlConnection con ; 
        private DataTable dt = new DataTable("tblOTO");
        private SqlDataAdapter da = new SqlDataAdapter();
        private void connect()
        {
    string cn =   "Data Source=BILLNG\\SQLEXPRESS;Initial Catalog= QLCARTA; Integrated Security=true";
            
            try
            {
                con = new SqlConnection(cn ); 
            }
            catch ( Exception ex)
            {
                MessageBox.Show ("khong the ket noi toi co so DL" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void disconnect()
        {
            con.Close();
            con.Dispose();
            con = null;
        } // dong ket noi , giai phong tn, huy doi tuong 
        void getdata()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(); // khai bao 1 command 
            cmd.Connection = con; //ket noi 
            cmd.CommandType = CommandType.Text; //khai bao kieu command 
            cmd.CommandText = @"SELECT * FROM oto";
            da.SelectCommand = cmd; //gan command cho da 
            da.Fill(dt); //nap du lieu cho table 
            
            
        }
        void getdata1()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM oto";
                da.SelectCommand = cmd;
                dt.Clear(); // Xóa dữ liệu cũ trong DataTable
                da.Fill(dt); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào hàng và cột hợp lệ
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Kiểm tra nếu cột nhấp chuột là cột 'code'
                if (dataGridView1.Columns[e.ColumnIndex].Name == "codeDataGridViewTextBoxColumn")
                {
                    // Lấy giá trị của ô được nhấn
                    string code = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    // Gọi phương thức để lấy thông tin chi tiết dựa trên mã code
                    getDetailsByCode(code); 
                }
            }
        }
        private void getDetailsByCode(string code)
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM oto WHERE code = @code";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@code", code); 

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["namecar"].ToString();
                    textBox2.Text = dt.Rows[0]["price"].ToString();
                }
                else
                {
                    MessageBox.Show("No data found for the code.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadComboBox()
        {
            try
            {
                con.Open();
                string query = "SELECT code FROM oto";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                comboBox1.Items.Clear(); // Xóa tất cả các mục hiện tại trong ComboBox

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["code"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có giá trị được chọn
            if (comboBox1.SelectedItem != null)
            {
                string selectedCode = comboBox1.SelectedItem.ToString();
                getDetailsByCode(selectedCode);
            }
        }
        //Trong phương thức getDetailsByCode, bạn đã thực hiện việc này rồi. Đảm bảo rằng các trường TextBox được cập nhật với thông tin tương ứng.
        public DuAn()
        {
            InitializeComponent();
            connect();
            getdata1();
            LoadComboBox(); // Gọi phương thức để nạp dữ liệu vào ComboBox
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            button2.Click += new EventHandler(button2_Click); // Đăng ký sự kiện Click cho button2
            button3.Click += new EventHandler(button3_Click); // Đăng ký sự kiện Click cho button2
            button4.Click += new EventHandler(button4_Click); // Đăng ký sự kiện Click cho button4
            button5.Click += new EventHandler(button5_Click);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi phương thức Fill trên TableAdapter để nạp dữ liệu vào DataSet
                otoTableAdapter.Fill(qLCARTADataSet.oto);

                // Gán DataSource cho DataGridView
                dataGridView1.DataSource = qLCARTADataSet.oto;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string code = comboBox1.Text;
            string name = textBox1.Text;
            string priceText = textBox2.Text;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Kiểm tra xem giá có phải là số hợp lệ không
            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Price must be a valid number.");
                return;
            }

            // Thực hiện thêm dữ liệu vào cơ sở dữ liệu
            try
            {
                con.Open();
                string query = "INSERT INTO oto (code, namecar, price) VALUES (@code, @namecar, @price)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@namecar", name);
                cmd.Parameters.AddWithValue("@price", price);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data added successfully.");
                    // Cập nhật lại ComboBox
                    LoadComboBox();
                }
                else
                {
                    MessageBox.Show("Failed to add data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Lấy mã code hiện tại từ ComboBox
            string code = comboBox1.Text;

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please select a code from the ComboBox.");
                return;
            }

            // Xác nhận xóa dữ liệu
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM oto WHERE code = @code";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@code", code);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data deleted successfully.");
                        // Cập nhật lại ComboBox
                        LoadComboBox();
                        // Xóa thông tin trên các TextBox
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete data.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // Lấy mã code hiện tại từ ComboBox
            string code = comboBox1.Text;

            // Lấy các giá trị mới từ các TextBox
            string newNameCar = textBox1.Text;
            string newPriceText = textBox2.Text;

            // Kiểm tra tính hợp lệ của giá trị giá xe
            if (!decimal.TryParse(newPriceText, out decimal newPrice))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("Please select a code from the ComboBox.");
                return;
            }

            // Xác nhận cập nhật dữ liệu
            DialogResult result = MessageBox.Show("Are you sure you want to update this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    con.Open();
                    string query = "UPDATE oto SET namecar = @namecar, price = @price WHERE code = @code";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@namecar", newNameCar);
                    cmd.Parameters.AddWithValue("@price", newPrice);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data updated successfully.");
                        // Cập nhật lại ComboBox
                        LoadComboBox();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update data.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            // Đóng form và thoát chương trình
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
