using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Hello_WindownForm
{
    public partial class matkhau : Form
    {
        private SqlConnection con;

        public matkhau()
        {
            InitializeComponent();
            // Khởi tạo kết nối cơ sở dữ liệu khi form khởi tạo
            string connectionString = "Data Source=BILLNG\\SQLEXPRESS;Initial Catalog=matkhau;Integrated Security=true";
            con = new SqlConnection(connectionString);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.Click += new System.EventHandler(this.button2_Click);
        }

        private void matkhau_Load(object sender, EventArgs e)
        {
            // Ví dụ: Kiểm tra kết nối cơ sở dữ liệu hoặc tải dữ liệu cần thiết
            if (!CheckDatabaseConnection())
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu.");
                this.Close(); // Đóng form nếu không thể kết nối
            }
        }

        private bool CheckDatabaseConnection()
        {
            try
            {
                con.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string taikhoan = textBox2.Text;
            string matkhau = textBox1.Text;

            if (AuthenticateUser(taikhoan, matkhau))
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Đóng form hiện tại và mở form DuAn
                this.Hide(); // Ẩn form hiện tại thay vì đóng
                DuAn duAnForm = new DuAn();
                duAnForm.FormClosed += (s, args) => this.Close(); // Đảm bảo form matkhau đóng khi DuAn bị đóng
                duAnForm.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.");
            }
        }

        private bool AuthenticateUser(string taikhoan, string matkhau)
        {
            try
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM matkhau1 WHERE taikhoan = @taikhoan AND matkhau = @matkhau";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                cmd.Parameters.AddWithValue("@matkhau", matkhau);

                int userCount = (int)cmd.ExecuteScalar();
                return userCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xác thực người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        private bool RegisterAccount(string taikhoan, string matkhau)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO matkhau1 (taikhoan, matkhau) VALUES (@taikhoan, @matkhau)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@taikhoan", taikhoan);
                cmd.Parameters.AddWithValue("@matkhau", matkhau);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex) when (ex.Number == 2627) // Violation of primary key constraint
            {
                MessageBox.Show("Tài khoản đã tồn tại. Vui lòng chọn tài khoản khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string taikhoan = textBox2.Text;
            string matkhau = textBox1.Text;

            // Kiểm tra và đăng ký tài khoản
            if (RegisterAccount(taikhoan, matkhau))
            {
                MessageBox.Show("Đăng ký thành công!");

                // Đóng form hiện tại và mở form DuAn
                this.Hide(); // Ẩn form hiện tại thay vì đóng
                DuAn duAnForm = new DuAn();
                duAnForm.FormClosed += (s, args) => this.Close(); // Đảm bảo form matkhau đóng khi DuAn bị đóng
                duAnForm.Show();
            }
        }
    }
}
