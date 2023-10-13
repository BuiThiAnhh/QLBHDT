using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;
using DTO;

namespace DAL
{
    public class DAL_HoaDonNhap:DBConnect
    {

        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            string sql = "Select * from HoaDonNhap";
            return connect.getData(sql);
        }
        public int kiemtramatrung(string ma)
        {
            string sql = "Select count(*) from HoaDonNhap where HoaDonNhapID='" + ma.Trim() + "'";
            return connect.kiemtramatrung(ma, sql);
        }
        public bool ThemHDN(HoaDonNhap hd)
        {
            string sql = string.Format("Insert into HoaDonNhap values('{0}','{1}', '{2}', '{3}', '{4}' ,'{5}' , '{6}')", hd.HoaDonNhapID, hd.MaSP , hd.MaNV, hd.DonGia, hd.SoLuong, hd.TongTien, hd.NgayNhap.ToString("yyyy/MM/dd"));
            thucthisql(sql);
            return true;
        }
        public bool SuaHDN(HoaDonNhap hd)
        {
            string sql = string.Format("UPDATE HoaDonNhap SET NgayNhap = '{0}', TongTien = '{1}', SoLuong = '{2}', DonGia = '{3}', MaNV = '{4}', MaSP = '{5}' WHERE HoaDonNhapID = '{6}'", hd.NgayNhap.ToString("yyyy-MM-dd"), hd.TongTien, hd.SoLuong, hd.DonGia, hd.MaNV, hd.MaSP, hd.HoaDonNhapID);
            thucthisql(sql);
            return true;
        }

        public bool XoaHDB(string ma)
        {
            string sql = "Delete from HoaDonNhap where HoaDonNhapID = '" + ma.Trim() + "'";
            thucthisql(sql);
            return true;
        }
    }
}
