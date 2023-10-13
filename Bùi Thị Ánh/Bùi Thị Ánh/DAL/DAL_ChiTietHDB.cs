using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;

namespace DAL
{
    public class DAL_ChiTietHDB : DBConnect
    {
        DBConnect connect = new DBConnect();
        public DataTable getData(string ma)
        {
            return connect.getData("Select * from ChiTietHoaDon Where MaHD = '" + ma.Trim() + "'");
        }
        public int kiemtramatrung(string ma)
        {
            string sql = "Select count(*) from ChiTietHoaDon where MaHD = '" + ma.Trim() + "'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemCTHDB(ChiTietHoaDonBan CT)
        {
            string sql = string.Format("Insert into ChiTietHoaDon Values ('{0}', '{1}', '{2}', '{3}')", CT.MaHD, CT.MaSP, CT.SoLuong, CT.DonGia);
          
            thucthisql(sql);
            
            return true;
        }
        public bool SuaCTHDB(ChiTietHoaDonBan CT)
        {
            string sql = string.Format("Update ChiTietHoaDon Set MaHD = '{0}' Where MaSP = '{1}', SoLuong = '{2}', DonGia = '{3}'", CT.MaHD, CT.MaSP, CT.SoLuong, CT.DonGia);
            thucthisql(sql);
            return true;
        }
        public bool XoaCTHDB(string ma)
        {
            string sql = "Delete from ChiTietHoaDon where MaHD = '" + ma.Trim() + "'";
            thucthisql(sql);
            
            return true;
        }
        public DataTable TimKiem(string mahd, string MaSp)
        {
            string sql = "Select * from ChiTietHoaDon";
            string dk = "";
            if (mahd.Trim() == "" && MaSp.Trim() == "")
            {
                // 1
                return null;
            }
            if (mahd.Trim() != "" && dk == "")
            {
                // 2
                dk += " MaHD like '%" + mahd.Trim() + "%'";
            }
            if (MaSp.Trim() != "" && dk != "")
            {
                // 3
                dk += " and SoNha like '%" + MaSp.Trim() + "%'";
            }
            if (MaSp.Trim() != "" && dk == "")
            {
                // 4
                dk += "SoNha like '%" + MaSp.Trim() + "%'";
            }
            if (dk != "")
            {
                sql += " WHERE " + dk;
            }
            return connect.getData(sql);
        }
        public double ThongKeDoanhThuTheoThang(string thang, string nam)
        {
            string sql = "SELECT SUM(TongTien) from HoaDon Where Month(NgayLap) = " + thang.Trim() + " and Year(NgayLap) = " + nam.Trim() + "";
            return connect.ThongKeDoanhThu(sql);
        }
        public DataTable ThongKeHoaDonTheoThang(string thang, string nam)
        {
            string sql = string.Format("Select H.MaHD, H.MaNV, H.MaKH, H.NgayLap, H.MaSP , H.SoLuong, H.DonGia from HoaDon H   Where Month(NgayLap) = " + thang.Trim() + " and Year(NgayLap) = " + nam.Trim() + "");
            return connect.getData(sql);
        }
        public DataTable ThongKeHoaDonTheoNam(string nam)
        {
            string sql = string.Format("Select c.MaHD, c.MaNV, c.MaKH, c.NgayLap, c.MaSP , c.SoLuong, c.DonGia  from HoaDon AS c  Where Year(NgayLap) = " + nam.Trim() + "");
            return connect.getData(sql);
        }
        public double ThongKeDoanhThuTheoNam(string nam)
        {
            string sql = "SELECT SUM(TongTien) from HoaDon Where Year(NgayLap) = " + nam.Trim() + "";
            return connect.ThongKeDoanhThu(sql);
        }
    }
}
