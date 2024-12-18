using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Bài_tập_lớn
{
    internal class ProcessDataBase
    {
        string strConnect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Chủ đề 1 Xây dựng ứng dụng Chat\\Chủ đề 1 Xây dựng ứng dụng Chat\\Bài tập lớn\\Database1.mdf\";Integrated Security=True";
        SqlConnection sqlConnect = null;
        //Hàm mở kết nối SQL
        private void KetNoiCSDL()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
                sqlConnect.Open();
        }
        //Hàm đóng kết nối SQL
        private void DongKetNoiCSDL()
        {
            if (sqlConnect.State != ConnectionState.Closed)
                sqlConnect.Close();
            sqlConnect.Dispose();
        }
        //Hàm thực thi câu lệnh dạng Select trả về DataTable
        public DataTable DocBang(string sql)
        {
            DataTable dtBang = new DataTable();
            KetNoiCSDL();
            SqlDataAdapter sqldataAdapte = new SqlDataAdapter(sql,
           sqlConnect);
            sqldataAdapte.Fill(dtBang);
            DongKetNoiCSDL();
            return dtBang;
        }
        //Hàm thực lệnh insert hoặc update hoặc delete
        public void CapNhatDuLieu(string sql, params SqlParameter[] parameters)
        {
            KetNoiCSDL();
            using (SqlCommand sqlcommand = new SqlCommand())
            {
                sqlcommand.Connection = sqlConnect;
                sqlcommand.CommandText = sql;
                if (parameters != null)
                {
                    sqlcommand.Parameters.AddRange(parameters);
                }
                sqlcommand.ExecuteNonQuery();
            }
            DongKetNoiCSDL();
        }


    }
}
