using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DAL
{
    public class DAL_HoaDon : DBConnect
    {
        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            string sql = "Select * from HoaDonBan";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = "Select count(*) from HoaDonBan where MaHD='" + ma.Trim() + "'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemHDB(HoaDon hd)
        {
            string sql = string.Format("Insert into HoaDonBan values('{0}','{1}', '{2}', '{3}', '{4}' ,'{5}' , '{6}' , '{7}' , '{8}')", hd.MaHD, hd.NgayLap.ToString("yyyy/MM/dd"), hd.TongTien,  hd.GhiChu, hd.MaKH, hd.MaNV , hd.MaSP  , hd.SoLuong , hd.DonGia);
            thucthisql(sql);
            return true;
        }
        public bool SuaHDB(HoaDon hd)
        {
            string sql = string.Format("UPDATE HoaDonBan SET NgayLap = '{0}', TongTien = '{1}', GhiChu = '{2}', MaKH = '{3}', MaNV = '{4}', MaSP = '{5}', SoLuong = '{6}', DonGia = '{7}' WHERE MaHD = '{8}'", hd.NgayLap.ToString("yyyy-MM-dd"), hd.TongTien, hd.GhiChu, hd.MaKH, hd.MaNV, hd.MaSP, hd.SoLuong, hd.DonGia, hd.MaHD);
            thucthisql(sql);
            return true;
        }

        public bool XoaHDB(string ma)
        {
            string sql = "Delete from HoaDonBan where MaHD = '" + ma.Trim() + "'";
            thucthisql(sql);
            return true;
        }
        public DataTable TimKiem(string mahdb, string manv, string makh)
        {
            string sql = "Select * from HoaDonBan";
            string dk = "";
            if (mahdb.Trim() == "" && manv.Trim() == "" && makh.Trim() == "")
            {
                // 1
                return null;
            }
            if (mahdb.Trim() != "" && dk == "")
            {
                // 2
                dk += " MaHD like '%" + mahdb.Trim() + "%' ";
            }
            if(manv.Trim() != "" && dk != "")
            {
                // 3
                dk += " and MaNV like '%" + manv.Trim() + "%' ";
            }
            if(manv.Trim() != "" && dk == "")
            {
                // 4
                dk += " MaNV like '%" + manv.Trim() + "%' ";
            }
            if(makh.Trim() != "" && dk != "")
            {
                // 5
                dk += " and MaKH like '%" + makh.Trim() + "%' ";
            }
            if(makh.Trim() != "" && dk == "")
            {
                dk += " MaKH like '%" + makh.Trim() + "%' ";
            }
            if(dk != "")
            {
                sql += " Where " + dk;
            }
            return connect.getData(sql);
        }
    }
}
