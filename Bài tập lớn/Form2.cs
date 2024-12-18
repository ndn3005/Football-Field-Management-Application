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
using Bài_tập_lớn.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace Bài_tập_lớn
{
    public partial class Form2 : Form
    {
        public string TenNguoiDung { set; get; }
        public Form2()
        {
            InitializeComponent();
        }

        string strConnect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Chủ đề 1 Xây dựng ứng dụng Chat\\Chủ đề 1 Xây dựng ứng dụng Chat\\Bài tập lớn\\Database1.mdf\";Integrated Security=True";
        ProcessDataBase dtBase = new ProcessDataBase();
        private void Form2_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(strConnect);
            con.Open();
            string q = "select * from [Đăng nhập] where TaiKhoan = @TaiKhoan";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@TaiKhoan", TenNguoiDung); // Use TenNguoiDung to get the account from Form1
            SqlDataReader dataReader = cmd.ExecuteReader();
            label2.Text = TenNguoiDung;

            if (dataReader.Read())
            {
                label2.Text = dataReader["Ten"].ToString(); // Display user's name in label2

                string Quyen = dataReader["Quyen"].ToString(); // Retrieve the user type from the database

                if (Quyen == "admin")
                {
                    panel2.Visible = true; // Show panel2 for admin users
                    panel3.Visible = false; // Hide panel3 for admin users
                    lblwelcomeadmin1.Visible = true;
                    lblwelcomeadmin2.Visible = true;
                    lblwelcomuser.Visible = false;
                    HideAllPanels();
                }
                else if (Quyen == "user")
                {
                    panel2.Visible = false; // Show panel2 for admin users
                    panel3.Visible = true; // Hide panel3 for admin users
                    lblwelcomeadmin1.Visible = true;
                    lblwelcomuser.Visible = true;
                    lblwelcomeadmin2.Visible = false;
                    HideAllPanels();

                }
            }
            con.Close();
        }
        private void HideAllPanels()
        {
            panqldatsan.Visible = false;
            panquanlysaanbai.Visible = false;
            pandichvubosung.Visible = false;
            panqlkhachhang.Visible = false;
            panquanlynhanvien.Visible = false;
            panQuanlihoadon.Visible = false;
            panBaoCaoDoanhThu.Visible = false;
            panDatSan.Visible = false;
            panDanhSachSan.Visible = false;
            panxemdsdv.Visible = false;
            panHoaDon.Visible = false;
            panqllsds.Visible = false;
            panlsdskh.Visible = false;
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnthemqlds_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO DatSan (MaKhachHang ,MaSan, TenKhachHang, SoDienThoai, NgayDat, GioBatDau, GioKetThuc) VALUES (@MaKhachHang,@MaSan, @TenKhachHang, @SoDienThoai, @NgayDat, @GioBatDau, @GioKetThuc)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtmakhachhangfqlds.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtmasanqlds.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txttenkhqlds.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtsdtqlds.Text);
                        cmd.Parameters.AddWithValue("@NgayDat", dtpngaydatqlds.Value);
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpgiobatdauqlds.Value);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpgioketthucqlds.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadData(); // Tải lại dữ liệu vào bảng
                txtmakhachhangfqlds.Text = "";
                txtmasanqlds.Text = "";
                txttenkhqlds.Text = "";
                txtsdtqlds.Text = "";
                dtpngaydatqlds.Value = DateTime.Now;
                dtpgiobatdauqlds.Value = DateTime.Now;
                dtpgioketthucqlds.Value= DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }

        private void btnsuaqlds_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE BangGiaGio SET LoaiSan = @LoaiSan, TuGio = @TuGio, DenGio = @DenGio, DonGia = @DonGia, MaKhachHang = @MaKhachHang WHERE MaSan = @MaSan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtmakhachhangfqlds.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtmasanqlds.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txttenkhqlds.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtsdtqlds.Text);
                        cmd.Parameters.AddWithValue("@NgayDat", dtpngaydatqlds.Value);
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpgiobatdauqlds.Value);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpgioketthucqlds.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Sửa thành công!");
                LoadData(); // Tải lại dữ liệu vào bảng
                txtmakhachhangfqlds.Text = "";
                txtmasanqlds.Text = "";
                txttenkhqlds.Text = "";
                txtsdtqlds.Text = "";
                dtpngaydatqlds.Value = DateTime.Now;
                dtpgiobatdauqlds.Value = DateTime.Now;
                dtpgioketthucqlds.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnxoaqlds_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "DELETE FROM BangGiaGio WHERE MaSan = @MaSan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", txtmasanqlds.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Xóa thành công!");
                LoadData(); // Tải lại dữ liệu vào bảng
                txtmakhachhangfqlds.Text = "";
                txtmasanqlds.Text = "";
                txttenkhqlds.Text = "";
                txtsdtqlds.Text = "";
                dtpngaydatqlds.Value = DateTime.Now;
                dtpgiobatdauqlds.Value = DateTime.Now;
                dtpgioketthucqlds.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM DatSan";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvqlds.DataSource = dt;
                        dgvqlds.Columns["MaKhachHang"].Width = 120; 
                        dgvqlds.Columns["MaSan"].Width = 80;
                        dgvqlds.Columns["TenKhachHang"].Width = 180;
                        dgvqlds.Columns["SoDienThoai"].Width = 120;
                        dgvqlds.Columns["NgayDat"].Width = 100;
                        dgvqlds.Columns["GioBatDau"].Width = 100;
                        dgvqlds.Columns["GioKetThuc"].Width = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }



        private void dgvqlds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvqlds.Rows[e.RowIndex].Cells["MaKhachHang"].Value != null)
                {
                    DataGridViewRow row = dgvqlds.Rows[e.RowIndex];

                    // Gán giá trị từ DataGridView vào các điều khiển
                    txtmakhachhangfqlds.Text = row.Cells["MaKhachHang"].Value?.ToString() ?? string.Empty;
                    txtmasanqlds.Text = row.Cells["MaSan"].Value?.ToString() ?? string.Empty;
                    txttenkhqlds.Text = row.Cells["TenKhachHang"].Value?.ToString() ?? string.Empty;
                    txtsdtqlds.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? string.Empty;

                    // Gán giá trị ngày
                    if (row.Cells["NgayDat"].Value != null && DateTime.TryParse(row.Cells["NgayDat"].Value.ToString(), out DateTime ngayDat))
                    {
                        dtpngaydatqlds.Value = ngayDat;
                    }
                    else
                    {
                        dtpngaydatqlds.Value = DateTime.Now;
                    }

                    // Gán giá trị giờ bắt đầu
                    if (row.Cells["GioBatDau"].Value != null && DateTime.TryParse(row.Cells["GioBatDau"].Value.ToString(), out DateTime tuGio))
                    {
                        dtpgiobatdauqlds.Value = tuGio;
                    }
                    else
                    {
                        dtpgiobatdauqlds.Value = DateTime.Now;
                    }

                    // Gán giá trị giờ kết thúc
                    if (row.Cells["GioKetThuc"].Value != null && DateTime.TryParse(row.Cells["GioKetThuc"].Value.ToString(), out DateTime denGio))
                    {
                        dtpgioketthucqlds.Value = denGio;
                    }
                    else
                    {
                        dtpgioketthucqlds.Value = DateTime.Now;
                    }
                }
                else
                {
                    // Nếu người dùng click vào dòng không hợp lệ hoặc dữ liệu không tồn tại
                    ResetFields();
                    MessageBox.Show("Không có dữ liệu hợp lệ tại dòng đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ResetFields();
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức đặt lại các điều khiển về giá trị mặc định
        private void ResetFields()
        {
            txtmakhachhangfqlds.Text = string.Empty;
            txtmasanqlds.Text = string.Empty;
            txttenkhqlds.Text = string.Empty;
            txtsdtqlds.Text = string.Empty;
            dtpngaydatqlds.Value = DateTime.Now;
            dtpgiobatdauqlds.Value = DateTime.Now;
            dtpgioketthucqlds.Value = DateTime.Now;
        }


        private void btnthemqlkh_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, DiaChi, SoDienThoai) VALUES (@MaKhachHang, @TenKhachHang, @DiaChi, @SoDienThoai)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add ".Text" to get the text value from the TextBox
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtMaKHqlkh.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKHqlkh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChiqlkh.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSDTqlkh.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqlkh(); // Reload the data to reflect changes
                txtMaKHqlkh.Text = "";
                txtTenKHqlkh.Text = "";
                txtDiaChiqlkh.Text = "";
                txtSDTqlkh.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }

        private void btnsuaqlkh_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu mã khách hàng trống
                if (string.IsNullOrEmpty(txtMaKHqlkh.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng để sửa.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE KhachHang SET TenKhachHang = @TenKhachHang, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai WHERE MaKhachHang = @MaKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtMaKHqlkh.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKHqlkh.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChiqlkh.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSDTqlkh.Text);

                        // Thực thi truy vấn
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!");
                            LoadDataqlkh(); // Tải lại dữ liệu vào bảng
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng để cập nhật.");
                        }
                    }
                }
                txtMaKHqlkh.Text = "";
                txtTenKHqlkh.Text = "";
                txtDiaChiqlkh.Text = "";
                txtSDTqlkh.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnxoaqlkh_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaKHqlkh.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng để xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xóa khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string query = "DELETE FROM KhachHang WHERE MaKhachHang = @MaKhachHang";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaKhachHang", txtMaKHqlkh.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadDataqlkh(); // Tải lại dữ liệu vào bảng
                }
                txtMaKHqlkh.Text = "";
                txtTenKHqlkh.Text = "";
                txtDiaChiqlkh.Text = "";
                txtSDTqlkh.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }
        private void LoadDataqlkh()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM KhachHang";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvdskh.DataSource = dt;
                        dgvdskh.Columns["MaKhachHang"].Width = 120;
                        dgvdskh.Columns["TenKhachHang"].Width = 180;
                        dgvdskh.Columns["DiaChi"].Width = 150;
                        dgvdskh.Columns["SoDienThoai"].Width = 120;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void dgvdskh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvdskh.Rows[e.RowIndex];
                txtMaKHqlkh.Text = row.Cells["MaKhachHang"].Value.ToString();
                txtTenKHqlkh.Text = row.Cells["TenKhachHang"].Value.ToString();
                txtDiaChiqlkh.Text = row.Cells["DiaChi"].Value.ToString();
                txtSDTqlkh.Text = row.Cells["SoDienThoai"].Value.ToString();

            }
        }

        private void LoadDataqlsb()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM SanBong";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvqlsb.DataSource = dt;
                        dgvqlsb.Columns["MaSan"].Width = 100;
                        dgvqlsb.Columns["TenSan"].Width = 100;
                        dgvqlsb.Columns["LoaiSan"].Width = 100;
                        dgvqlsb.Columns["Gia"].Width = 100;
                        dgvqlsb.Columns["TrangThai"].Width = 120;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnThemqlsb_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO SanBong (MaSan, TenSan, LoaiSan, Gia, TrangThai) VALUES (@MaSan, @TenSan, @LoaiSan, @Gia, @TrangThai)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add ".Text" to get the text value from the TextBox
                        cmd.Parameters.AddWithValue("@MaSan", txtMaSanqlsb.Text);
                        cmd.Parameters.AddWithValue("@TenSan", txtTenSanqlsb.Text);
                        cmd.Parameters.AddWithValue("@LoaiSan", txtLoaiSanqlsb.Text);
                        cmd.Parameters.AddWithValue("@Gia", txtGiaqlsb.Text);
                        cmd.Parameters.AddWithValue("@TrangThai", txtTrangThaiqlsb.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqlsb(); // Reload the data to reflect changes
                txtMaSanqlsb.Text = "";
                txtTenSanqlsb.Text = "";
                txtLoaiSanqlsb.Text = "";
                txtGiaqlsb.Text = "";
                txtTrangThaiqlsb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }

        private void btnSuaqlsb_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSanqlsb.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sân để sửa!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE SanBong SET TenSan = @TenSan, LoaiSan = @LoaiSan, Gia = @Gia, TrangThai = @TrangThai WHERE MaSan = @MaSan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", txtMaSanqlsb.Text); // Điều kiện xác định sân cần sửa
                        cmd.Parameters.AddWithValue("@TenSan", txtTenSanqlsb.Text);
                        cmd.Parameters.AddWithValue("@LoaiSan", txtLoaiSanqlsb.Text);
                        cmd.Parameters.AddWithValue("@Gia", txtGiaqlsb.Text);
                        cmd.Parameters.AddWithValue("@TrangThai", txtTrangThaiqlsb.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật thông tin sân bóng thành công!");
                LoadDataqlsb(); // Làm mới dữ liệu sau khi sửa
                txtMaSanqlsb.Text = "";
                txtTenSanqlsb.Text = "";
                txtLoaiSanqlsb.Text = "";
                txtGiaqlsb.Text = "";
                txtTrangThaiqlsb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoaqlsb_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaSanqlsb.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sân để xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sân bóng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string query = "DELETE FROM SanBong WHERE MaSan = @MaSan";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaSan", txtMaSanqlsb.Text); // Điều kiện xác định sân cần xóa
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa sân bóng thành công!");
                    LoadDataqlsb(); // Làm mới dữ liệu sau khi xóa

                }
                txtMaSanqlsb.Text = "";
                txtTenSanqlsb.Text = "";
                txtLoaiSanqlsb.Text = "";
                txtGiaqlsb.Text = "";
                txtTrangThaiqlsb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void dgvqlsb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvqlsb.Rows[e.RowIndex];
                txtMaSanqlsb.Text = row.Cells["MaSan"].Value.ToString();
                txtTenSanqlsb.Text = row.Cells["TenSan"].Value.ToString();
                txtLoaiSanqlsb.Text = row.Cells["LoaiSan"].Value.ToString();
                txtGiaqlsb.Text = row.Cells["Gia"].Value.ToString();
                txtTrangThaiqlsb.Text = row.Cells["TrangThai"].Value.ToString();
            }
        }
        private void LoadDataqldv()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM DichVu";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvqldv.DataSource = dt;
                        dgvqldv.Columns["MaDichVu"].Width = 145;
                        dgvqldv.Columns["TenDichVu"].Width = 250;
                        dgvqldv.Columns["DonGia"].Width = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnThemqldv_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia) VALUES (@MaDichVu, @TenDichVu, @DonGia)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add ".Text" to get the text value from the TextBox
                        cmd.Parameters.AddWithValue("@MaDichVu", txtMaDVqldv.Text);
                        cmd.Parameters.AddWithValue("@TenDichVu", txtTenDVqldv.Text);
                        cmd.Parameters.AddWithValue("@DonGia", txtDonGiaqldv.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqldv(); // Reload the data to reflect changes
                txtMaDVqldv.Text = "";
                txtTenDVqldv.Text = "";
                txtDonGiaqldv.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }

        private void btnSuaqldv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaDVqldv.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã dịch vụ để sửa!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE DichVu SET TenDichVu = @TenDichVu, DonGia = @DonGia WHERE MaDichVu = @MaDichVu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDichVu", txtMaDVqldv.Text); // Điều kiện xác định dịch vụ cần sửa
                        cmd.Parameters.AddWithValue("@TenDichVu", txtTenDVqldv.Text);
                        cmd.Parameters.AddWithValue("@DonGia", txtDonGiaqldv.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật dịch vụ thành công!");
                LoadDataqldv(); // Làm mới dữ liệu sau khi sửa
                txtMaDVqldv.Text = "";
                txtTenDVqldv.Text = "";
                txtDonGiaqldv.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoaqldv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaDVqldv.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã dịch vụ để xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string query = "DELETE FROM DichVu WHERE MaDichVu = @MaDichVu";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaDichVu", txtMaDVqldv.Text); // Điều kiện xác định dịch vụ cần xóa
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa dịch vụ thành công!");
                    LoadDataqldv(); // Làm mới dữ liệu sau khi xóa
                    txtMaDVqldv.Text = "";
                    txtTenDVqldv.Text = "";
                    txtDonGiaqldv.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void dgvqldv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvqldv.Rows[e.RowIndex];
            txtMaDVqldv.Text = row.Cells["MaDichVu"].Value.ToString();
            txtTenDVqldv.Text = row.Cells["TenDichVu"].Value.ToString();
            txtDonGiaqldv.Text = row.Cells["DonGia"].Value.ToString();

        }
        private void LoadDataqlnv()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM NhanVien";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvqlnv.DataSource = dt;
                        dgvqlnv.Columns["MaNhanVien"].Width = 110;
                        dgvqlnv.Columns["TenNhanVien"].Width = 170;
                        dgvqlnv.Columns["DiaChi"].Width = 120;
                        dgvqlnv.Columns["SoDienThoai"].Width = 120;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnThemqlnv_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, DiaChi, SoDienThoai) VALUES (@MaNhanVien, @TenNhanVien, @DiaChi, @SoDienThoai)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add ".Text" to get the text value from the TextBox
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNVqlnv.Text);
                        cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNVqlnv.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChiqlnv.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSDTqlnv.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqlnv(); // Reload the data to reflect changes
                txtMaNVqlnv.Text = "";
                txtTenNVqlnv.Text = "";
                txtDiaChiqlnv.Text = "";
                txtSDTqlnv.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }

        private void btnSuaqlnv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNVqlnv.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên để sửa!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE NhanVien SET TenNhanVien = @TenNhanVien, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai WHERE MaNhanVien = @MaNhanVien";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNVqlnv.Text); // Điều kiện xác định nhân viên cần sửa
                        cmd.Parameters.AddWithValue("@TenNhanVien", txtTenNVqlnv.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChiqlnv.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSDTqlnv.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                LoadDataqlnv(); // Làm mới dữ liệu sau khi sửa
                txtMaNVqlnv.Text = "";
                txtTenNVqlnv.Text = "";
                txtDiaChiqlnv.Text = "";
                txtSDTqlnv.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNVqlnv.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên để xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string query = "DELETE FROM NhanVien WHERE MaNhanVien = @MaNhanVien";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNVqlnv.Text); // Điều kiện xác định nhân viên cần xóa
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadDataqlnv(); // Làm mới dữ liệu sau khi xóa
                    txtMaNVqlnv.Text = "";
                    txtTenNVqlnv.Text = "";
                    txtDiaChiqlnv.Text = "";
                    txtSDTqlnv.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void dgvqlnv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvqlnv.Rows[e.RowIndex];
            txtMaNVqlnv.Text = row.Cells["MaNhanVien"].Value.ToString();
            txtTenNVqlnv.Text = row.Cells["TenNhanVien"].Value.ToString();
            txtDiaChiqlnv.Text = row.Cells["DiaChi"].Value.ToString();
            txtSDTqlnv.Text = row.Cells["SoDienThoai"].Value.ToString();

        }
        private void LoadDataqlhd()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvqlhd.DataSource = dt;
                    }

                    // Định dạng cột NgayTao (nếu cần thiết)
                    dgvqlhd.Columns["NgayTaoHoaDon"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThemqlhd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO HoaDon (MaHoaDon, TenKhachHang, SoDienThoai, TongTien, MaSan, NgayTaoHoaDon, TenDichVu, TrangThai) VALUES (@MaHoaDon, @TenKhachHang, @SoDienThoai, @TongTien, @MaSan, @NgayTaoHoaDon, @TenDichVu, @TrangThai)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", txtMaHDqlhd.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHangqlhd.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSDTqlhd.Text);
                        cmd.Parameters.AddWithValue("@TongTien", txtTongTienqlhd.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtMaSanqlhd.Text);
                        cmd.Parameters.AddWithValue("@NgayTaoHoaDon", dtpNgayTaoHD.Value);

                        // Kiểm tra giá trị của cboDichVuBoSung
                        object tenDichVu = cboDichVuBoSung.SelectedValue ?? DBNull.Value;
                        cmd.Parameters.AddWithValue("@TenDichVu", cboDichVuBoSung.SelectedItem);
                        cmd.Parameters.AddWithValue("@TrangThai", txttrangthai.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqlhd();
                txtMaHDqlhd.Text = "";
                txtTenKhachHangqlhd.Text = "";
                txtSDTqlhd.Text = "";
                txtTongTienqlhd.Text = "";
                txtMaSanqlhd.Text = "";
                dtpNgayTaoHD.Value = DateTime.Now;
                cboDichVuBoSung.SelectedIndex = -1;
                txttrangthai.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }


        private void btnSuaqlhd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaHDqlhd.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã hóa đơn để sửa!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE HoaDon SET TenKhachHang = @TenKhachHang, SoDienThoai = @SoDienThoai, TongTien = @TongTien, MaSan = @MaSan , NgayTaoHoaDon = @NgayTaoHoaDon , TenDichVu = @TenDichVu , TrangThai = @TrangThai WHERE MaHoaDon = @MaHoaDon";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Điều kiện xác định hóa đơn cần sửa
                        cmd.Parameters.AddWithValue("@MaHoaDon", txtMaHDqlhd.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHangqlhd.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSDTqlhd.Text);
                        cmd.Parameters.AddWithValue("@TongTien", txtTongTienqlhd.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtMaSanqlhd.Text);
                        cmd.Parameters.AddWithValue("@NgayTaoHoaDon", dtpNgayTaoHD.Value);

                        // Gán giá trị cho @TenDichVu
                        if (cboDichVuBoSung.SelectedItem != null)
                        {
                            cmd.Parameters.AddWithValue("@TenDichVu", cboDichVuBoSung.Text); // Lấy giá trị hiển thị của ComboBox
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TenDichVu", DBNull.Value); // Nếu không có giá trị, truyền NULL
                        }
                        cmd.Parameters.AddWithValue("@TrangThai", txttrangthai.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cập nhật hóa đơn thành công!");
                LoadDataqlhd(); // Làm mới dữ liệu sau khi sửa
                txtMaHDqlhd.Text = "";
                txtTenKhachHangqlhd.Text = "";
                txtSDTqlhd.Text = "";
                txtTongTienqlhd.Text = "";
                txtMaSanqlhd.Text = "";
                dtpNgayTaoHD.Value = DateTime.Now;
                cboDichVuBoSung.SelectedIndex = -1; // Reset ComboBox
                txttrangthai.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }



        private void btnXoaqlhd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaHDqlhd.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã hóa đơn để xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaHoaDon", txtMaHDqlhd.Text); // Điều kiện xác định hóa đơn cần xóa
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa hóa đơn thành công!");
                    LoadDataqlhd(); // Làm mới dữ liệu sau khi xóa
                    txtMaHDqlhd.Text = "";
                    txtTenKhachHangqlhd.Text = "";
                    txtSDTqlhd.Text = "";
                    txtTongTienqlhd.Text = "";
                    txtMaSanqlhd.Text = "";
                    dtpNgayTaoHD.Value = DateTime.Now;
                    cboDichVuBoSung.SelectedIndex = -1;
                    txttrangthai.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void dgvqlhd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) // Chỉ thực thi khi click vào dòng hợp lệ
                {
                    DataGridViewRow row = dgvqlhd.Rows[e.RowIndex];

                    // Gán giá trị từ DataGridView vào các TextBox
                    txtMaHDqlhd.Text = row.Cells["MaHoaDon"].Value?.ToString() ?? string.Empty;
                    txtTenKhachHangqlhd.Text = row.Cells["TenKhachHang"].Value?.ToString() ?? string.Empty;
                    txtSDTqlhd.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? string.Empty;
                    txtTongTienqlhd.Text = row.Cells["TongTien"].Value?.ToString() ?? string.Empty;
                    txtMaSanqlhd.Text = row.Cells["MaSan"].Value?.ToString() ?? string.Empty;

                    // Gán giá trị cho DateTimePicker
                    if (row.Cells["NgayTaoHoaDon"].Value != DBNull.Value && row.Cells["NgayTaoHoaDon"].Value != null)
                    {
                        dtpNgayTaoHD.Value = Convert.ToDateTime(row.Cells["NgayTaoHoaDon"].Value);
                    }
                    else
                    {
                        dtpNgayTaoHD.Value = DateTime.Now; // Nếu không có giá trị, gán ngày hiện tại
                    }

                    // Gán giá trị cho ComboBox dựa trên TenDichVu
                    string tenDichVu = row.Cells["TenDichVu"].Value?.ToString() ?? string.Empty;
                    if (!string.IsNullOrEmpty(tenDichVu))
                    {
                        cboDichVuBoSung.SelectedIndex = cboDichVuBoSung.FindStringExact(tenDichVu); // Tìm giá trị phù hợp
                    }
                    else
                    {
                        cboDichVuBoSung.SelectedIndex = -1; // Không có dịch vụ bổ sung
                    }
                    txttrangthai.Text = row.Cells["TrangThai"].Value?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadDataSanBong()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM SanBong";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvDanhSachSan.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void LoadDataDichVu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM DichVu";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvxemdsdv.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtMaKhachHangds.Clear();
            txtMaSanBongds.Clear();
            txtTenKhachHang.Clear();
            txtSoDienThoai.Clear();
            dtpNgayDat.Value = DateTime.Now;
            dtpbatdau.Value = DateTime.Now;
            dtpketthuc.Value = DateTime.Now;
        }

        private void btnDatSands_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO DatSan (MaKhachHang, MaSan, TenKhachHang, SoDienThoai, NgayDat, GioBatDau, GioKetThuc) " +
                                   "VALUES (@MaKhachHang, @MaSan, @TenKhachHang, @SoDienThoai, @NgayDat, @GioBatDau, @GioKetThuc)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtMaKhachHangds.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtMaSanBongds.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txtTenKhachHang.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                        cmd.Parameters.AddWithValue("@NgayDat", dtpNgayDat.Value);
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpbatdau.Value);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpketthuc.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Gọi hàm để tải dữ liệu vừa thêm và gán vào các Label trong panelHoaDon


                MessageBox.Show("Bạn đã đặt sân thành công, Vui lòng đợi cuộc gọi xác nhận");
                txtMaKhachHangds.Text = "";
                txtMaSanBongds.Text = "";
                txtTenKhachHang.Text = "";
                txtSoDienThoai.Text = "";
                dtpNgayDat.Value= DateTime.Now;
                dtpbatdau.Value= DateTime.Now;
                dtpketthuc.Value= DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }
       
        private void LoadDataHOADON()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvHOADON.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }


        private void dgvHOADON_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra nếu người dùng click vào hàng hợp lệ
                if (e.RowIndex >= 0 && dgvHOADON.Rows[e.RowIndex].Cells["MaHoaDon"].Value != null)
                {
                    DataGridViewRow row = dgvHOADON.Rows[e.RowIndex];

                    // Gán giá trị từ DataGridView vào các Label
                    lblMHD.Text = row.Cells["MaHoaDon"].Value?.ToString() ?? string.Empty;
                    lblTKH.Text = row.Cells["TenKhachHang"].Value?.ToString() ?? string.Empty;
                    lblSDT.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? string.Empty;
                    lblTT.Text = row.Cells["TongTien"].Value?.ToString() ?? string.Empty;
                    lblMS.Text = row.Cells["MaSan"].Value?.ToString() ?? string.Empty;
                    lbldvbshd.Text = row.Cells["TenDichVu"].Value?.ToString() ?? string.Empty;
                    lbltrangthaihoadon.Text = row.Cells["TrangThai"].Value?.ToString() ?? string.Empty;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hàng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBaoCaoDoanhThu(string type)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();

                    // Truy vấn tổng hợp doanh thu chỉ tính những hóa đơn đã thanh toán
                    string query = @"
        SELECT 
            " + type + @"(NgayTaoHoaDon) AS [Thời Gian], 
            SUM(TongTien) AS [Tổng Doanh Thu]
        FROM HoaDon
        WHERE TrangThai = 'Đã thanh toán'  -- Thêm điều kiện trạng thái
        GROUP BY " + type + @"(NgayTaoHoaDon)
        ORDER BY [Thời Gian]";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBaoCaoDoanhThu.DataSource = dt;

                        // Định dạng hiển thị
                        dgvBaoCaoDoanhThu.Columns["Tổng Doanh Thu"].DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioNgay.Checked)
            {
                LoadBaoCaoDoanhThu("DAY");
            }
            else if (radioThang.Checked)
            {
                LoadBaoCaoDoanhThu("MONTH");
            }
            else if (radioNam.Checked)
            {
                LoadBaoCaoDoanhThu("YEAR");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn kiểu báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnqldatsan_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panqldatsan.Visible = true;
            LoadData();
        }

        private void btnqlkhachhang_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panqlkhachhang.Visible = true;
            LoadDataqlkh();
        }

        private void btnqlsanbai_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panquanlysaanbai.Visible = true;
            LoadDataqlsb();
        }

        private void btnqldichvubosung_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            pandichvubosung.Visible = true;
            LoadDataqldv();
        }

        private void btnqlnhanvien_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panquanlynhanvien.Visible = true;
            LoadDataqlnv();
        }

        private void btnqlhoadon_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panQuanlihoadon.Visible = true;
            LoadDataqlhd();
        }

        private void btnbaocaodoanhthu_Click(object sender, EventArgs e)
        {
            radioNgay.Checked = true; // Mặc định chọn "Theo Ngày"
            LoadBaoCaoDoanhThu("DAY");
            HideAllPanels();
            panBaoCaoDoanhThu.Visible = true;
        }

        private void btndangxuat_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void btndatsan_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panDatSan.Visible = true;
        }

        private void btnxemdanhsachsan_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panDanhSachSan.Visible = true;
            LoadDataSanBong();
        }

        private void btndanhsachdv_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panxemdsdv.Visible = true;
            LoadDataDichVu();
        }

        private void btnhoadon_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panHoaDon.Visible = true;
            LoadDataHOADON();
        }

        private void btndangxuatt_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void btntimkiemqlds_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();

                    // Câu truy vấn SQL để tìm kiếm theo Tên khách hàng
                    string query = "SELECT MaKhachHang ,MaSan, TenKhachHang, SoDienThoai, NgayDat, GioBatDau, GioKetThuc " +
                                   "FROM DatSan " +
                                   "WHERE TenKhachHang LIKE @TenKhachHang";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số cho Tên khách hàng
                        cmd.Parameters.AddWithValue("@TenKhachHang", "%" + txttimkiemqlds.Text.Trim() + "%");

                        // Thực hiện truy vấn và đọc dữ liệu
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Hiển thị kết quả lên DataGridView
                            dgvqlds.DataSource = dt;

                            // Kiểm tra nếu không có kết quả
                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Không tìm thấy dữ liệu với tên khách hàng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgvqlds.DataSource = null; // Xóa dữ liệu cũ nếu không có kết quả
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btntimkhachhang_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemkhachhang.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã khách hàng để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM KhachHang WHERE MaKhachHang LIKE @MaKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", "%" + txttimkiemkhachhang.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvdskh.DataSource = dt;
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng.");
                                dgvdskh.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btntimkiemsan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemsan.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sân để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM SanBong WHERE MaSan LIKE @MaSan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSan", "%" + txttimkiemsan.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvqlsb.DataSource = dt;
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sân bóng.");
                                dgvqlsb.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btntimkiemdvbs_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemdvbs.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã dịch vụ để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM DichVu WHERE MaDichVu LIKE @MaDichVu";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDichVu", "%" + txttimkiemdvbs.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvqldv.DataSource = dt;
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy dịch vụ.");
                                dgvqldv.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btntimkiemhd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemhd.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã hóa đơn để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon WHERE MaHoaDon LIKE @MaHoaDon";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHoaDon", "%" + txttimkiemhd.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvqlhd.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hóa đơn.");
                                dgvqlhd.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btntimkiemnv_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemnv.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM NhanVien WHERE MaNhanVien LIKE @MaNhanVien";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNhanVien", "%" + txttimkiemnv.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvqlnv.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy nhân viên.");
                                dgvqlnv.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnrs_Click(object sender, EventArgs e)
        {
            txtmakhachhangfqlds.Text = "";
            txtmasanqlds.Text = "";
            txttenkhqlds.Text = "";
            txtsdtqlds.Text = "";
            dtpngaydatqlds.Value = DateTime.Now;
            dtpgiobatdauqlds.Value = DateTime.Now;
            dtpgioketthucqlds.Value = DateTime.Now;
            LoadData();
        }

        private void btnrskh_Click(object sender, EventArgs e)
        {
            txtMaKHqlkh.Text = "";
            txtTenKHqlkh.Text = "";
            txtDiaChiqlkh.Text = "";
            txtSDTqlkh.Text = "";
            LoadDataqlkh();
        }

        private void btnrssb_Click(object sender, EventArgs e)
        {
            txtMaSanqlsb.Text = "";
            txtTenSanqlsb.Text = "";
            txtLoaiSanqlsb.Text = "";
            txtGiaqlsb.Text = "";
            txtTrangThaiqlsb.Text = "";
            LoadDataqlsb();
        }

        private void btnrsdv_Click(object sender, EventArgs e)
        {
            LoadDataqldv();
            txtMaDVqldv.Text = "";
            txtTenDVqldv.Text = "";
            txtDonGiaqldv.Text = "";
        }

        private void btnrsnv_Click(object sender, EventArgs e)
        {
            LoadDataqlnv();
            txtMaNVqlnv.Text = "";
            txtTenNVqlnv.Text = "";
            txtDiaChiqlnv.Text = "";
            txtSDTqlnv.Text = "";
        }

        private void btnrshd_Click(object sender, EventArgs e)
        {
            LoadDataqlhd(); 
            txtMaHDqlhd.Text = "";
            txtTenKhachHangqlhd.Text = "";
            txtSDTqlhd.Text = "";
            txtTongTienqlhd.Text = "";
            txtMaSanqlhd.Text = "";
            dtpNgayTaoHD.Value = DateTime.Now;
            cboDichVuBoSung.SelectedIndex = -1;
        }

        private void LoadDataqllsds()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM DatSan";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvqllsds.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnthemlsds_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "INSERT INTO DatSan (MaKhachHang, MaSan, TenKhachHang, SoDienThoai, NgayDat, GioBatDau, GioKetThuc) VALUES (@MaKhachHang, @MaSan, @TenKhachHang, @SoDienThoai, @NgayDat, @GioBatDau, @GioKetThuc)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add ".Text" to get the text value from the TextBox
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtmkhlsds.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtmslsds.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txttkhlsds.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtsdtlsds.Text);
                        cmd.Parameters.AddWithValue("@NgayDat", dtpngaydatlsds.Value);
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpgiobatdaulsds.Value);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpgioketthuclsds.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqllsds(); // Reload the data to reflect changes
                txtmkhlsds.Text = "";
                txtmslsds.Text = "";
                txttkhlsds.Text = "";
                txtsdtlsds.Text = "";
                dtpngaydatlsds.Value = DateTime.Now;
                dtpgiobatdaulsds.Value = DateTime.Now;
                dtpgioketthuclsds.Value = DateTime.Now;
                txttimkiemtheoten.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm mới: " + ex.Message);
            }
        }

        private void btnsualsds_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaHDqlhd.Text))
                {
                    MessageBox.Show("Vui lòng chọn để sửa!");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "UPDATE DatSan SET MaSan = @MaSan, TenKhachHang = @TenKhachHang, SoDienThoai = @SoDienThoai, NgayDat = @NgayDat, GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc WHERE MaKhachHang = @MaKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhachHang", txtmkhlsds.Text);
                        cmd.Parameters.AddWithValue("@MaSan", txtmslsds.Text);
                        cmd.Parameters.AddWithValue("@TenKhachHang", txttkhlsds.Text);
                        cmd.Parameters.AddWithValue("@SoDienThoai", txtsdtlsds.Text);
                        cmd.Parameters.AddWithValue("@NgayDat", dtpngaydatlsds.Value);
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpgiobatdaulsds.Value);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpgioketthuclsds.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm mới thành công!");
                LoadDataqllsds(); // Reload the data to reflect changes
                txtmkhlsds.Text = "";
                txtmslsds.Text = "";
                txttkhlsds.Text = "";
                txtsdtlsds.Text = "";
                dtpngaydatlsds.Value = DateTime.Now;
                dtpgiobatdaulsds.Value = DateTime.Now;
                dtpgioketthuclsds.Value = DateTime.Now;
                txttimkiemtheoten.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnxoalsds_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmkhlsds.Text))
                {
                    MessageBox.Show("Vui lòng chọn để xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn đặt sân này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(strConnect))
                    {
                        conn.Open();
                        string query = "DELETE FROM DatSan WHERE MaKhachHang = @MaKhachHang";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaKhachHang", txtmkhlsds.Text); // Điều kiện xác định hóa đơn cần xóa
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Xóa thành công!");
                    LoadDataqllsds(); // Reload the data to reflect changes
                    txtmkhlsds.Text = "";
                    txtmslsds.Text = "";
                    txttkhlsds.Text = "";
                    txtsdtlsds.Text = "";
                    dtpngaydatlsds.Value = DateTime.Now;
                    dtpgiobatdaulsds.Value = DateTime.Now;
                    dtpgioketthuclsds.Value = DateTime.Now;
                    txttimkiemtheoten.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void btntimkiemlsds_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemtheoten.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();

                    // Sửa query để tìm kiếm theo tên khách hàng
                    string query = "SELECT * FROM DatSan WHERE TenKhachHang LIKE @TenKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Sử dụng tên khách hàng làm tham số tìm kiếm
                        cmd.Parameters.AddWithValue("@TenKhachHang", "%" + txttimkiemtheoten.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvqllsds.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng.");
                                dgvqllsds.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
                txttimkiemtheoten.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvqllsds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0) // Only proceed if a valid row is clicked
                {
                    DataGridViewRow row = dgvqllsds.Rows[e.RowIndex];

                    // Set TextBox values from the selected row's cells
                    txtmkhlsds.Text = row.Cells["MaKhachHang"].Value?.ToString() ?? string.Empty;
                    txtmslsds.Text = row.Cells["MaSan"].Value?.ToString() ?? string.Empty;
                    txttkhlsds.Text = row.Cells["TenKhachHang"].Value?.ToString() ?? string.Empty;
                    txtsdtlsds.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? string.Empty;

                    // Set DateTimePicker values from the selected row's cells

                    // For NgayDat (Booking Date) - full date
                    if (row.Cells["NgayDat"].Value != DBNull.Value && row.Cells["NgayDat"].Value != null)
                    {
                        dtpngaydatlsds.Value = Convert.ToDateTime(row.Cells["NgayDat"].Value);
                    }
                    else
                    {
                        dtpngaydatlsds.Value = DateTime.Now; // Set current date if no value
                    }

                    // For GioBatDau (Start Time) - Time format only
                    if (row.Cells["GioBatDau"].Value != DBNull.Value && row.Cells["GioBatDau"].Value != null)
                    {
                        // Convert the value to DateTime and use only the time part
                        TimeSpan startTime = (TimeSpan)row.Cells["GioBatDau"].Value;
                        dtpgiobatdaulsds.Value = DateTime.Today.Add(startTime); // Use DateTime.Today to keep the date part as today
                    }
                    else
                    {
                        dtpgiobatdaulsds.Value = DateTime.Now; // Set current time if no value
                    }

                    // For GioKetThuc (End Time) - Time format only
                    if (row.Cells["GioKetThuc"].Value != DBNull.Value && row.Cells["GioKetThuc"].Value != null)
                    {
                        // Convert the value to DateTime and use only the time part
                        TimeSpan endTime = (TimeSpan)row.Cells["GioKetThuc"].Value;
                        dtpgioketthuclsds.Value = DateTime.Today.Add(endTime); // Use DateTime.Today to keep the date part as today
                    }
                    else
                    {
                        dtpgioketthuclsds.Value = DateTime.Now; // Set current time if no value
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnresetlsds_Click(object sender, EventArgs e)
        {
            LoadDataqllsds(); // Reload the data to reflect changes
            txtmkhlsds.Text = "";
            txtmslsds.Text = "";
            txttkhlsds.Text = "";
            txtsdtlsds.Text = "";
            dtpngaydatlsds.Value = DateTime.Now;
            dtpgiobatdaulsds.Value = DateTime.Now;
            dtpgioketthuclsds.Value = DateTime.Now;
            txttimkiemtheoten.Text = "";
        }

        private void btnqllsds_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panqllsds.Visible = true;
            LoadDataqllsds();
        }

        private void btntimkiemlsdskh_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemlsdskh.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();

                    // Sửa query để tìm kiếm theo tên khách hàng
                    string query = "SELECT * FROM DatSan WHERE TenKhachHang LIKE @TenKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Sử dụng tên khách hàng làm tham số tìm kiếm
                        cmd.Parameters.AddWithValue("@TenKhachHang", "%" + txttimkiemlsdskh.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvlsdskh.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khách hàng.");
                                dgvlsdskh.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
                txttimkiemlsdskh.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void btnlsds_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            panlsdskh.Visible = true;
        }

        private void txtTongTienqlhd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void TinhTongTien()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();

                    // Lấy giá sân dựa trên Mã sân
                    decimal donGiaSan = 0;
                    if (!string.IsNullOrEmpty(txtMaSanqlhd.Text) && int.TryParse(txtMaSanqlhd.Text, out int maSan))
                    {
                        string querySan = "SELECT Gia FROM SanBong WHERE MaSan = @MaSan";
                        using (SqlCommand cmd = new SqlCommand(querySan, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaSan", maSan);
                            var resultSan = cmd.ExecuteScalar();
                            donGiaSan = resultSan != null ? Convert.ToDecimal(resultSan) : 0;
                        }
                    }

                    // Lấy giá dịch vụ dựa trên Mã dịch vụ
                    decimal donGiaDichVu = 0;
                    if (cboDichVuBoSung.SelectedItem != null)
                    {
                        string tenDichVu = cboDichVuBoSung.Text; // Lấy Tên dịch vụ từ ComboBox
                        string queryDichVu = "SELECT DonGia FROM DichVu WHERE TenDichVu = @TenDichVu";
                        using (SqlCommand cmd = new SqlCommand(queryDichVu, conn))
                        {
                            cmd.Parameters.AddWithValue("@TenDichVu", tenDichVu);
                            var resultDichVu = cmd.ExecuteScalar();
                            donGiaDichVu = resultDichVu != null ? Convert.ToDecimal(resultDichVu) : 0;
                        }
                    }
                    // Tính tổng tiền
                    decimal tongTien = donGiaSan + donGiaDichVu;

                    // Hiển thị tổng tiền
                    txtTongTienqlhd.Text = tongTien.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void LoadDichVu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT TenDichVu FROM DichVu";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cboDichVuBoSung.DataSource = dt;
                        cboDichVuBoSung.DisplayMember = "TenDichVu"; // Hiển thị tên dịch vụ
                        cboDichVuBoSung.ValueMember = "TenDichVu";   // Giá trị là tên dịch vụ
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }


        private void txtMaSanqlhd_TextChanged(object sender, EventArgs e)
        {
            TinhTongTien(); // Tính lại tổng tiền khi mã sân thay đổi
        }

        private void cboDichVuBoSung_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhTongTien(); // Tính lại tổng tiền khi dịch vụ thay đổi
        }

        private void btntimkiemhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu ô nhập liệu tìm kiếm trống
                if (string.IsNullOrEmpty(txttimkiemhoadonkhachhang.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng để tìm kiếm.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(strConnect))
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon WHERE TenKhachHang LIKE @TenKhachHang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenKhachHang", "%" + txttimkiemhoadonkhachhang.Text + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Kiểm tra nếu có dữ liệu
                            if (dt.Rows.Count > 0)
                            {
                                dgvHOADON.DataSource = dt; // Hiển thị dữ liệu lên DataGridView
                                MessageBox.Show("Tìm kiếm thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy hóa đơn với tên khách hàng này.");
                                dgvHOADON.DataSource = null; // Xóa dữ liệu cũ nếu không tìm thấy
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
    }
}
