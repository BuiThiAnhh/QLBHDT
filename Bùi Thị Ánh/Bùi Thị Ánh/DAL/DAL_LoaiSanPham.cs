using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DAL
{
    public class DAL_LoaiSanPham : DBConnect
    {
        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            return connect.getData("Select * from LoaiSanPham");
        }
        public int kiemtramatrung(string ma)
        {
            string sql = "Select count(*) from LoaiSanPham where MaLoai = '" + ma.Trim() + "'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemDayCH(LoaiSanPham dch)
        {
            string sql = string.Format("Insert into LoaiSanPham Values ('{0}';, N'{1}')", dch.MaLoai, dch.TenLoai);
            thucthisql(sql);
            return true;
        }
        public bool SuaDCH(LoaiSanPham dch)
        {
            string sql = "Update LoaiSanPham Set TenLoai = N'" + dch.TenLoai + "' Where MaLoai = '" + dch.MaLoai + "'";
            thucthisql(sql);
            return true;
        }
        public bool XoaDCH(string ma)
        {
            string sql = "Delete from LoaiSanPham Where MaLoai = '" + ma.Trim() + "'";
            thucthisql(sql);
            return true;
        }
        public string loadcbbDCH(int ma)
        {
            string sql = "Select TenLoai From LoaiSanPham Where MaLoai = '" + ma+ "'";
            return connect.valuecbb(ma, sql);
        }
    }
}
