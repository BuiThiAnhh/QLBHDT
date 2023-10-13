using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_TaiKhoan : DBConnect
    {
       

        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            return connect.getData("Select * from TaiKhoan");
        }

        public bool KiemTraDangNhap(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=BUIANH\MSSQLSERVER02;Initial Catalog=BuiThiAnh_Project;Integrated Security=True"))
            {
                connection.Open(); // Mở kết nối

                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE UserName = @UserName AND PassWord = @PassWord";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@PassWord", password);
                int count = Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
        }


        public int kiemtramatrung(string ma)
        {
            string sql = "Select count(*) From TaiKhoan Where UserName = '" + ma.Trim() + "'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemUser(TaiKhoan tk)
        {
            string sql = "INSERT INTO TaiKhoan VALUES('" + tk.UserName + "','" + tk.PassWord + "')";
            thucthisql(sql);
            return true;
        }
    }
}
