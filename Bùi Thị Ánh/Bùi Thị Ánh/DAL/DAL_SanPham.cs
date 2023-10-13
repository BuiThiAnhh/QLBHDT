using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DAL
{
    public class DAL_SanPham : DBConnect
    {
        //chứa mọi xử lý liên quan đến CSDL
        //xử lý lấy dữ liệu để đưa ra dgv
        //khởi tạo đối tượng thuộc lớp DBConnect
        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            string sql = "Select * from SanPham";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = "Select count(*) from SanPham where MaSP = '" + ma + "'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemCH(SanPham ch)
        {
            string sql = string.Format("Insert into SanPham values('{0}',N'{1}', '{2}', '{3}', '{4}', '{5}' ,'{6}','{7}',{8})", ch.MaSP, ch.TenSP, ch.GiaBan, ch.SoLuong, ch.NgayNhap.ToString("yyyy-MM-dd") , ch.MaNCC , ch.MaLoai, ch.HeDieuHanhID, ch.HangID);
            thucthisql(sql);
            return true;
        }
        public bool SuaCH(SanPham ch)
        {
            string sql = string.Format("Update SanPham set TenSP = N'{1}', GiaBan = '{2}', SoLuong = '{3}', MaNCC = '{4}', MaLoai ='{5}' , HeDieuHanhID = '{6}' , HangID = '{7}' Where MaSP = '{0}' ", ch.MaSP, ch.TenSP, ch.GiaBan, ch.SoLuong, ch.MaNCC , ch.MaLoai, ch.HeDieuHanhID, ch.HangID);
            thucthisql(sql);
            return true;
        }
        public bool XoaCH(int ma)
        {
            string sql = "DELETE from SanPham Where MaSP = '" + ma + "'";
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiemCH(string MaSP, string ten, string GiaBan)
        {
            string sql = "SELECT * FROM SanPham";
            string dk = "";

            if (MaSP.Trim() == "" && ten.Trim() == "" && GiaBan.Trim() == "")
            {
                return null;
            }

            if (MaSP.Trim() != "")
            {
                dk += " MaSP = " + MaSP.Trim() + " AND";
            }

            if (ten.Trim() != "")
            {
                dk += " TenSP LIKE N'%" + ten.Trim() + "%' AND";
            }

            if (GiaBan.Trim() != "")
            {
                dk += " GiaBan BETWEEN " + (float.Parse(GiaBan) - 500000) + " AND " + (float.Parse(GiaBan) + 500000) + " AND";
            }

            if (dk != "")
            {
                sql += " WHERE" + dk.Remove(dk.Length - 3);
            }

            return connect.getData(sql);
        }

    }
}
