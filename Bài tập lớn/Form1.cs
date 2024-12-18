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
using System.IO;
namespace Bài_tập_lớn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Color btn = Color.MediumSeaGreen;
        string strConnect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Chủ đề 1 Xây dựng ứng dụng Chat\\Chủ đề 1 Xây dựng ứng dụng Chat\\Bài tập lớn\\Database1.mdf\";Integrated Security=True";
        private void btndangnhap_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
            btndangky.BackColor = Color.Gray;
            btndangnhap.BackColor = btn;
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
            btndangnhap.BackColor = Color.Gray;
            btndangky.BackColor = btn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btndangnhap.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Check for empty fields
            if (string.IsNullOrWhiteSpace(txtHo.Text) ||
                string.IsNullOrWhiteSpace(txtTen.Text) ||
                string.IsNullOrWhiteSpace(txtTaiKhoan.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtXacNhan.Text) ||
                string.IsNullOrWhiteSpace(txtQuyen.Text)) // Ensure role/privilege field is filled
            {
                MessageBox.Show("Thiếu thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if passwords match
            if (txtMatKhau.Text != txtXacNhan.Text)
            {
                MessageBox.Show("Mật khẩu bạn nhập không trùng khớp!");
                return;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(strConnect))
                {
                    // Adjusted SQL query to include Quyen parameter
                    string q = "INSERT INTO [Đăng nhập] (Ho, Ten, TaiKhoan, MatKhau, XacNhan, Quyen) " +
                               "VALUES (@Ho, @Ten, @TaiKhoan, @MatKhau, @XacNhan, @Quyen)";
                    SqlCommand cmd = new SqlCommand(q, con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Ho", txtHo.Text);
                    cmd.Parameters.AddWithValue("@Ten", txtTen.Text);
                    cmd.Parameters.AddWithValue("@TaiKhoan", txtTaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                    cmd.Parameters.AddWithValue("@XacNhan", txtXacNhan.Text);
                    cmd.Parameters.AddWithValue("@Quyen", txtQuyen.Text); // Add user role (Admin/User)

                    try
                    {
                        // Open the connection and execute the query
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Bạn đã đăng ký thành công!", "Thông báo");

                        // Clear fields after successful registration
                        txtHo.Clear();
                        txtTen.Clear();
                        txtTaiKhoan.Clear();
                        txtMatKhau.Clear();
                        txtXacNhan.Clear();
                        txtQuyen.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đăng ký thất bại: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }




        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoandn.Text))
            {
                MessageBox.Show("Bạn chưa nhập tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMatKhaudn.Text))
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(strConnect))
            {
                con.Open();
                string q = "SELECT * FROM [Đăng nhập] WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.AddWithValue("@TaiKhoan", txtTaiKhoandn.Text);
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatKhaudn.Text);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (!dataReader.HasRows)
                        {
                            MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác, vui lòng nhập lại", "Thông báo");
                        }
                        else
                        {
                            // Handle successful login
                            Form2 f2 = new Form2();
                            f2.TenNguoiDung = txtTaiKhoandn.Text;
                            this.Hide();
                            f2.Show();
                        }
                    }
                }con.Close();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtXacNhan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
